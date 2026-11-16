package rw.auca.lectrolend.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import rw.auca.lectrolend.dto.request.EquipmentRequest;
import rw.auca.lectrolend.dto.response.AvailabilityDto;
import rw.auca.lectrolend.dto.response.EquipmentDto;
import rw.auca.lectrolend.dto.response.Slot;
import rw.auca.lectrolend.exception.BadRequestException;
import rw.auca.lectrolend.exception.NotFoundException;
import rw.auca.lectrolend.model.Equipment;
import rw.auca.lectrolend.model.enums.EquipmentStatus;
import rw.auca.lectrolend.model.enums.EquipmentType;
import rw.auca.lectrolend.model.enums.ReservationStatus;
import rw.auca.lectrolend.repository.AdminRepository;
import rw.auca.lectrolend.repository.EquipmentRepository;
import rw.auca.lectrolend.repository.ReservationRepository;

import java.time.LocalDateTime;
import java.util.Comparator;
import java.util.List;

@Service
@RequiredArgsConstructor
public class EquipmentService {

    private final EquipmentRepository equipment;
    private final ReservationRepository reservations;
    private final AdminRepository admins;

    static EquipmentDto toDto(Equipment e) {
        return new EquipmentDto(e.getEquipmentId(), e.getName(), e.getType(), e.getAssetTag(),
                e.getStatus(), e.getDescription(), e.isReservable());
    }

    @Transactional(readOnly = true)
    public List<EquipmentDto> list(EquipmentType type, EquipmentStatus status, String q) {
        String needle = q == null ? "" : q.trim().toLowerCase();
        return equipment.findAll().stream()
                .filter(e -> type == null || e.getType() == type)
                .filter(e -> status == null || e.getStatus() == status)
                .filter(e -> needle.isEmpty() || e.getName().toLowerCase().contains(needle)
                        || e.getAssetTag().toLowerCase().contains(needle))
                .sorted(Comparator.comparing(Equipment::getName).thenComparing(Equipment::getAssetTag))
                .map(EquipmentService::toDto)
                .toList();
    }

    @Transactional(readOnly = true)
    public EquipmentDto get(Long id) {
        return toDto(find(id));
    }

    /** Availability for a time window plus the upcoming busy slots (no personal details exposed). */
    @Transactional(readOnly = true)
    public AvailabilityDto availability(Long id, LocalDateTime start, LocalDateTime end) {
        Equipment e = find(id);
        List<Slot> busy = reservations.findUpcoming(id, LocalDateTime.now(), ReservationService.ACTIVE).stream()
                .map(r -> new Slot(r.getStartTime(), r.getEndTime())).toList();
        if (!e.isReservable()) {
            return new AvailabilityDto(false, "This item is " + e.getStatus().name().toLowerCase().replace('_', ' ')
                    + " and cannot be reserved", busy);
        }
        if (start != null && end != null) {
            if (!end.isAfter(start)) return new AvailabilityDto(false, "End time must be after start time", busy);
            long clashes = reservations.countOverlaps(id, start, end, ReservationService.ACTIVE);
            if (clashes > 0) return new AvailabilityDto(false, "Already booked for part of that time", busy);
        }
        return new AvailabilityDto(true, null, busy);
    }

    @Transactional
    public EquipmentDto create(Long adminId, EquipmentRequest req) {
        String tag = req.assetTag().trim();
        if (equipment.existsByAssetTagIgnoreCase(tag)) throw new BadRequestException("Asset tag already exists");
        Equipment e = new Equipment();
        e.setAddedBy(admins.getReferenceById(adminId));
        apply(e, req);
        e.setStatus(req.status() == null ? EquipmentStatus.AVAILABLE : req.status());
        return toDto(equipment.save(e));
    }

    @Transactional
    public EquipmentDto update(Long id, EquipmentRequest req) {
        Equipment e = find(id);
        String tag = req.assetTag().trim();
        if (!e.getAssetTag().equalsIgnoreCase(tag) && equipment.existsByAssetTagIgnoreCase(tag)) {
            throw new BadRequestException("Asset tag already exists");
        }
        apply(e, req);
        if (req.status() != null && req.status() != e.getStatus()) changeStatus(e, req.status());
        return toDto(e);
    }

    @Transactional
    public EquipmentDto updateStatus(Long id, EquipmentStatus status) {
        Equipment e = equipment.findByIdForUpdate(id).orElseThrow(() -> new NotFoundException("Equipment not found"));
        changeStatus(e, status);
        return toDto(e);
    }

    /** Removes unused equipment; items with history are retired instead so past records stay intact. */
    @Transactional
    public EquipmentDto remove(Long id) {
        Equipment e = find(id);
        if (reservations.existsByEquipment_EquipmentIdAndStatusIn(id, ReservationService.ACTIVE)) {
            throw new IllegalStateException("This item has active reservations. Resolve them before removing it.");
        }
        if (reservations.existsByEquipment_EquipmentId(id)) {
            e.updateStatus(EquipmentStatus.RETIRED);
            return toDto(e);
        }
        equipment.delete(e);
        return null;
    }

    private void changeStatus(Equipment e, EquipmentStatus status) {
        boolean itemIsOut = reservations.existsByEquipment_EquipmentIdAndStatusIn(e.getEquipmentId(),
                List.of(ReservationStatus.ISSUED, ReservationStatus.OVERDUE));
        if (itemIsOut && status != EquipmentStatus.IN_USE) {
            throw new IllegalStateException("This item is currently issued. Confirm its return first.");
        }
        if (status == EquipmentStatus.IN_USE && !itemIsOut) {
            throw new IllegalStateException("IN_USE is set automatically when an item is issued");
        }
        e.updateStatus(status);
    }

    private void apply(Equipment e, EquipmentRequest req) {
        e.setName(req.name().trim());
        e.setType(req.type());
        e.setAssetTag(req.assetTag().trim());
        e.setDescription(req.description() == null || req.description().isBlank() ? null : req.description().trim());
    }

    private Equipment find(Long id) {
        return equipment.findById(id).orElseThrow(() -> new NotFoundException("Equipment not found"));
    }
}
