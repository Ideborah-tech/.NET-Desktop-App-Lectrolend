package rw.auca.lectrolend.service;

import lombok.RequiredArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.access.AccessDeniedException;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import rw.auca.lectrolend.dto.request.BatchReservationRequest;
import rw.auca.lectrolend.dto.request.ReservationRequest;
import rw.auca.lectrolend.dto.request.ReturnRequest;
import rw.auca.lectrolend.dto.response.BorrowerInfo;
import rw.auca.lectrolend.dto.response.EquipmentInfo;
import rw.auca.lectrolend.dto.response.ReservationDto;
import rw.auca.lectrolend.dto.response.ReturnInfo;
import rw.auca.lectrolend.dto.response.RoomInfo;
import rw.auca.lectrolend.exception.BadRequestException;
import rw.auca.lectrolend.exception.NotFoundException;
import rw.auca.lectrolend.exception.ReservationConflictException;
import rw.auca.lectrolend.model.Borrower;
import rw.auca.lectrolend.model.Equipment;
import rw.auca.lectrolend.model.Reservation;
import rw.auca.lectrolend.model.ReturnRecord;
import rw.auca.lectrolend.model.Room;
import rw.auca.lectrolend.model.User;
import rw.auca.lectrolend.model.enums.EquipmentStatus;
import rw.auca.lectrolend.model.enums.ReservationStatus;
import rw.auca.lectrolend.repository.*;

import java.time.Duration;
import java.time.LocalDateTime;
import java.util.List;

/**
 * Core business rules: availability, double-booking prevention, and the reservation life cycle
 * (BOOKED -> ISSUED -> RETURNED / OVERDUE, CANCELLED, EXPIRED) from the State Diagram.
 */
@Service
@RequiredArgsConstructor
public class ReservationService {

    private static final Logger log = LoggerFactory.getLogger(ReservationService.class);

    /** Statuses that occupy the equipment for their time window. */
    public static final List<ReservationStatus> ACTIVE =
            List.of(ReservationStatus.BOOKED, ReservationStatus.ISSUED, ReservationStatus.OVERDUE);

    private static final Duration MAX_DURATION = Duration.ofDays(7);
    private static final Duration PAST_TOLERANCE = Duration.ofMinutes(5);

    private final ReservationRepository reservations;
    private final EquipmentRepository equipment;
    private final RoomRepository rooms;
    private final UserRepository users;
    private final AdminRepository admins;
    private final ReturnRecordRepository returnRecords;

    @Value("${lectrolend.reservation.pickup-window-minutes}")
    private long pickupWindowMinutes;
    @Value("${lectrolend.reservation.early-pickup-minutes}")
    private long earlyPickupMinutes;

    // ---- queries ----------------------------------------------------------------------------

    @Transactional(readOnly = true)
    public List<ReservationDto> mine(Long userId) {
        LocalDateTime now = LocalDateTime.now();
        return reservations.findByUser_UserIdOrderByStartTimeDesc(userId).stream().map(r -> toDto(r, now)).toList();
    }

    @Transactional(readOnly = true)
    public List<ReservationDto> all(ReservationStatus status) {
        LocalDateTime now = LocalDateTime.now();
        List<Reservation> list = status == null
                ? reservations.findAllByOrderByStartTimeDesc()
                : reservations.findByStatusOrderByStartTimeDesc(status);
        return list.stream().map(r -> toDto(r, now)).toList();
    }

    // ---- make a reservation (Sequence Diagram) ----------------------------------------------

    @Transactional
    public ReservationDto create(Long userId, ReservationRequest req) {
        return createBatch(userId, new BatchReservationRequest(List.of(req.equipmentId()), req.startTime(), req.endTime(), req.roomId())).get(0);
    }

    /** All-or-nothing: one reservation per item for the same slot, in a single transaction. */
    @Transactional
    public List<ReservationDto> createBatch(Long userId, BatchReservationRequest req) {
        User user = users.findById(userId).orElseThrow(() -> new NotFoundException("User not found"));
        if (!(user instanceof Borrower borrower)) {
            throw new AccessDeniedException("Only students and lecturers can make reservations");
        }
        LocalDateTime now = LocalDateTime.now();
        validateWindow(req.startTime(), req.endTime(), now);

        List<Long> ids = req.equipmentIds().stream().distinct().sorted().toList(); // fixed lock order avoids deadlocks
        long active = reservations.countByUser_UserIdAndStatusIn(userId, ACTIVE);
        if (active + ids.size() > borrower.getMaxActiveReservations()) {
            throw new ReservationConflictException("You hold " + active + " active reservation(s) and the limit is "
                    + borrower.getMaxActiveReservations() + ", so you can add at most "
                    + Math.max(0, borrower.getMaxActiveReservations() - active) + " more.");
        }
        Room room = req.roomId() == null ? null
                : rooms.findById(req.roomId()).orElseThrow(() -> new NotFoundException("Room not found"));

        List<Reservation> created = new java.util.ArrayList<>();
        for (Long id : ids) {
            Equipment e = equipment.findByIdForUpdate(id).orElseThrow(() -> new NotFoundException("Equipment not found"));
            if (!e.isReservable()) {
                throw new ReservationConflictException(e.getName() + " is " + e.getStatus().name().toLowerCase().replace('_', ' ')
                        + " and cannot be reserved");
            }
            if (reservations.countOverlaps(e.getEquipmentId(), req.startTime(), req.endTime(), ACTIVE) > 0) {
                throw new ReservationConflictException(e.getName() + " is already booked for part of that time. Choose another time.");
            }
            Reservation r = new Reservation();
            r.setUser(user);
            r.setEquipment(e);
            r.setRoom(room);
            r.setStartTime(req.startTime());
            r.setEndTime(req.endTime());
            r.setStatus(ReservationStatus.BOOKED);
            r.setCreatedAt(now);
            created.add(reservations.save(r));
            log.info("Reservation {} created by user {} for equipment {}", r.getReservationId(), userId, e.getAssetTag());
        }
        return created.stream().map(r -> toDto(r, now)).toList();
    }

    // ---- cancel ------------------------------------------------------------------------------

    @Transactional
    public ReservationDto cancel(Long reservationId, Long actorId, boolean actorIsAdmin) {
        Reservation r = find(reservationId);
        if (!actorIsAdmin && !r.getUser().getUserId().equals(actorId)) {
            throw new AccessDeniedException("You can only cancel your own reservations");
        }
        r.cancel();
        return toDto(r, LocalDateTime.now());
    }

    // ---- issue at pick-up --------------------------------------------------------------------

    @Transactional
    public ReservationDto issue(Long reservationId) {
        Reservation r = find(reservationId);
        LocalDateTime now = LocalDateTime.now();
        if (r.getStatus() != ReservationStatus.BOOKED) {
            throw new IllegalStateException("Only a BOOKED reservation can be issued (this one is " + r.getStatus() + ")");
        }
        if (now.isBefore(r.getStartTime().minusMinutes(earlyPickupMinutes))) {
            throw new ReservationConflictException("Too early. Pick-up opens " + earlyPickupMinutes
                    + " minutes before the start time.");
        }
        if (now.isAfter(r.getStartTime().plusMinutes(pickupWindowMinutes))) {
            throw new ReservationConflictException("The " + pickupWindowMinutes
                    + "-minute pick-up window has passed. The reservation will be marked EXPIRED.");
        }
        Equipment e = equipment.findByIdForUpdate(r.getEquipment().getEquipmentId()).orElseThrow();
        if (!e.isAvailable()) {
            throw new ReservationConflictException(e.getName() + " is " + e.getStatus().name().toLowerCase().replace('_', ' ')
                    + " and cannot be handed over right now.");
        }
        r.markIssued(now);
        e.updateStatus(EquipmentStatus.IN_USE);
        return toDto(r, now);
    }

    // ---- confirm return ----------------------------------------------------------------------

    @Transactional
    public ReservationDto confirmReturn(Long reservationId, Long adminId, ReturnRequest req) {
        Reservation r = find(reservationId);
        LocalDateTime now = LocalDateTime.now();
        if (r.getStatus() != ReservationStatus.ISSUED && r.getStatus() != ReservationStatus.OVERDUE) {
            throw new IllegalStateException("Only an ISSUED or OVERDUE reservation can be returned (this one is "
                    + r.getStatus() + ")");
        }
        boolean damaged = req != null && req.damaged();
        boolean late = now.isAfter(r.getEndTime());

        String condition = req != null && req.conditionOnReturn() != null && !req.conditionOnReturn().isBlank()
                ? req.conditionOnReturn().trim() : (damaged ? "Damaged" : "Good");
        if (condition.length() > 100) {
            throw new BadRequestException("Condition must be 100 characters or fewer");
        }
        if (returnRecords.findByReservation_ReservationId(reservationId).isPresent()) {
            throw new IllegalStateException("A return has already been recorded for this reservation");
        }

        ReturnRecord rec = new ReturnRecord();
        rec.setReservation(r);
        rec.setConfirmedBy(admins.getReferenceById(adminId));
        rec.setReturnedAt(now);
        rec.setConditionOnReturn(condition);
        rec.setLateReturn(late);
        rec.setRemarks(req == null || req.remarks() == null || req.remarks().isBlank() ? null : req.remarks().trim());
        returnRecords.save(rec);
        r.setReturnRecord(rec);

        r.markReturned();
        Equipment e = equipment.findByIdForUpdate(r.getEquipment().getEquipmentId()).orElseThrow();
        e.updateStatus(damaged ? EquipmentStatus.UNDER_MAINTENANCE : EquipmentStatus.AVAILABLE);
        return toDto(r, now);
    }

    // ---- scheduled housekeeping --------------------------------------------------------------

    /** Expires uncollected bookings and flags late items as OVERDUE. Runs every minute. */
    @Transactional
    public void runHousekeeping() {
        LocalDateTime now = LocalDateTime.now();
        for (Reservation r : reservations.findByStatusAndStartTimeBefore(ReservationStatus.BOOKED,
                now.minusMinutes(pickupWindowMinutes))) {
            r.expire();
            log.info("Reservation {} expired (not collected within {} minutes)", r.getReservationId(), pickupWindowMinutes);
        }
        for (Reservation r : reservations.findByStatusAndEndTimeBefore(ReservationStatus.ISSUED, now)) {
            r.markOverdue();
            log.warn("Reservation {} is OVERDUE: {} not returned by {}", r.getReservationId(),
                    r.getEquipment().getAssetTag(), r.getEndTime());
        }
    }

    // ---- helpers -----------------------------------------------------------------------------

    private void validateWindow(LocalDateTime start, LocalDateTime end, LocalDateTime now) {
        if (!end.isAfter(start)) throw new BadRequestException("End time must be after the start time");
        if (start.isBefore(now.minus(PAST_TOLERANCE))) throw new BadRequestException("Start time cannot be in the past");
        if (Duration.between(start, end).compareTo(MAX_DURATION) > 0) {
            throw new BadRequestException("A reservation cannot be longer than 7 days");
        }
    }

    private Reservation find(Long id) {
        return reservations.findById(id).orElseThrow(() -> new NotFoundException("Reservation not found"));
    }

    private ReservationDto toDto(Reservation r, LocalDateTime now) {
        User u = r.getUser();
        Equipment e = r.getEquipment();
        Room room = r.getRoom();
        ReturnRecord rr = r.getReturnRecord();
        return new ReservationDto(
                r.getReservationId(),
                new BorrowerInfo(u.getUserId(), u.getFullName(), u.getEmail(), u.getRole().name()),
                new EquipmentInfo(e.getEquipmentId(), e.getName(), e.getAssetTag(), e.getType(), e.getStatus()),
                room == null ? null : new RoomInfo(room.getRoomId(), room.getRoomCode(), room.getBuilding()),
                r.getCreatedAt(), r.getStartTime(), r.getEndTime(), r.getStatus(), r.getIssuedAt(),
                r.isOverdue(now),
                rr == null ? null : new ReturnInfo(rr.getReturnedAt(), rr.getConditionOnReturn(), rr.isLateReturn(),
                        rr.getRemarks(), rr.getConfirmedBy().getFullName()));
    }
}
