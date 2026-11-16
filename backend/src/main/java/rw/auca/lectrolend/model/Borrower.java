package rw.auca.lectrolend.model;

/**
 * Abstract generalisation of Student and Lecturer. It has no table of its own; the shared
 * behaviour (borrowing limits) is inherited by both concrete borrower types.
 */
public abstract class Borrower extends User {

    /** Maximum number of reservations a borrower may hold at once (BOOKED / ISSUED / OVERDUE). */
    public abstract int getMaxActiveReservations();
}
