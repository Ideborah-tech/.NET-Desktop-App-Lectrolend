package rw.auca.lectrolend.exception;

/** Equipment unavailable or time slot already booked (HTTP 409, as in the sequence diagram). */
public class ReservationConflictException extends RuntimeException {
    public ReservationConflictException(String message) { super(message); }
}
