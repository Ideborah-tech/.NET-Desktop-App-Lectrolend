package rw.auca.lectrolend.model;

import org.junit.jupiter.api.Test;
import rw.auca.lectrolend.model.enums.ReservationStatus;
import rw.auca.lectrolend.model.enums.Role;

import java.time.LocalDateTime;

import static org.junit.jupiter.api.Assertions.*;

/** Verifies the reservation state machine from the State Diagram. */
class ReservationStateTest {

    private Reservation booked() {
        Reservation r = new Reservation();
        r.setStartTime(LocalDateTime.of(2026, 1, 1, 10, 0));
        r.setEndTime(LocalDateTime.of(2026, 1, 1, 12, 0));
        return r;
    }

    @Test
    void bookedCanBeIssuedThenReturned() {
        Reservation r = booked();
        r.markIssued(LocalDateTime.of(2026, 1, 1, 10, 5));
        assertEquals(ReservationStatus.ISSUED, r.getStatus());
        r.markReturned();
        assertEquals(ReservationStatus.RETURNED, r.getStatus());
    }

    @Test
    void issuedBecomesOverdueAndCanStillBeReturned() {
        Reservation r = booked();
        r.markIssued(LocalDateTime.of(2026, 1, 1, 10, 5));
        r.markOverdue();
        assertTrue(r.isOverdue(LocalDateTime.of(2026, 1, 1, 13, 0)));
        r.markReturned();
        assertEquals(ReservationStatus.RETURNED, r.getStatus());
    }

    @Test
    void onlyBookedCanBeCancelledOrExpired() {
        Reservation r = booked();
        r.markIssued(LocalDateTime.now());
        assertThrows(IllegalStateException.class, r::cancel);
        assertThrows(IllegalStateException.class, r::expire);
    }

    @Test
    void cannotIssueTwiceOrReturnBeforeIssue() {
        Reservation r = booked();
        assertThrows(IllegalStateException.class, r::markReturned);
        r.markIssued(LocalDateTime.now());
        assertThrows(IllegalStateException.class, () -> r.markIssued(LocalDateTime.now()));
    }

    @Test
    void overlapDetectionIsExclusiveAtTheEdges() {
        Reservation r = booked();
        assertTrue(r.overlaps(LocalDateTime.of(2026, 1, 1, 11, 0), LocalDateTime.of(2026, 1, 1, 13, 0)));
        assertFalse(r.overlaps(LocalDateTime.of(2026, 1, 1, 12, 0), LocalDateTime.of(2026, 1, 1, 13, 0)));
        assertFalse(r.overlaps(LocalDateTime.of(2026, 1, 1, 8, 0), LocalDateTime.of(2026, 1, 1, 10, 0)));
    }

    @Test
    void studentAndLecturerInheritBorrowerLimits() {
        Borrower s = new Student();
        Borrower l = new Lecturer();
        assertEquals(3, s.getMaxActiveReservations());
        assertEquals(5, l.getMaxActiveReservations());
        assertEquals(Role.STUDENT, s.getRole());
    }
}
