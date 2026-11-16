package rw.auca.lectrolend.model;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;
import rw.auca.lectrolend.model.enums.ReservationStatus;

import java.time.LocalDateTime;

@Entity
@Table(name = "reservation")
@Getter
@Setter
public class Reservation {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "reservation_id")
    private Long reservationId;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "user_id", nullable = false)
    private User user;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "equipment_id", nullable = false)
    private Equipment equipment;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "room_id")
    private Room room;

    @Column(name = "created_at", nullable = false)
    private LocalDateTime createdAt = LocalDateTime.now();

    @Column(name = "start_time", nullable = false)
    private LocalDateTime startTime;

    @Column(name = "end_time", nullable = false)
    private LocalDateTime endTime;

    @Enumerated(EnumType.STRING)
    @Column(nullable = false, length = 20)
    private ReservationStatus status = ReservationStatus.BOOKED;

    @Column(name = "issued_at")
    private LocalDateTime issuedAt;

    @OneToOne(mappedBy = "reservation", fetch = FetchType.LAZY)
    private ReturnRecord returnRecord;

    // ---- state machine (see State Diagram) ---------------------------------------------------

    public void cancel() {
        require(status == ReservationStatus.BOOKED, "Only a BOOKED reservation can be cancelled before pick-up");
        status = ReservationStatus.CANCELLED;
    }

    public void markIssued(LocalDateTime now) {
        require(status == ReservationStatus.BOOKED, "Only a BOOKED reservation can be issued");
        status = ReservationStatus.ISSUED;
        issuedAt = now;
    }

    public void markReturned() {
        require(status == ReservationStatus.ISSUED || status == ReservationStatus.OVERDUE,
                "Only an ISSUED or OVERDUE reservation can be returned");
        status = ReservationStatus.RETURNED;
    }

    public void expire() {
        require(status == ReservationStatus.BOOKED, "Only a BOOKED reservation can expire");
        status = ReservationStatus.EXPIRED;
    }

    public void markOverdue() {
        require(status == ReservationStatus.ISSUED, "Only an ISSUED reservation can become OVERDUE");
        status = ReservationStatus.OVERDUE;
    }

    public boolean isOverdue(LocalDateTime now) {
        return (status == ReservationStatus.ISSUED || status == ReservationStatus.OVERDUE) && now.isAfter(endTime);
    }

    public boolean overlaps(LocalDateTime start, LocalDateTime end) {
        return startTime.isBefore(end) && endTime.isAfter(start);
    }

    private static void require(boolean ok, String message) {
        if (!ok) throw new IllegalStateException(message);
    }
}
