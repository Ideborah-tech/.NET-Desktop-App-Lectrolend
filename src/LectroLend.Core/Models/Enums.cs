namespace LectroLend.Core.Models;

// An enum is a fixed list of possible values.

// The three kinds of users.
public enum Role
{
    STUDENT,
    LECTURER,
    ADMIN
}

// The kinds of equipment the IT office lends.
public enum EquipmentType
{
    PROJECTOR,
    HDMI_CABLE,
    REMOTE_CONTROL,
    OTHER
}

// Where a piece of equipment is right now.
public enum EquipmentStatus
{
    AVAILABLE,          // on the shelf, ready to lend
    IN_USE,             // someone has it
    UNDER_MAINTENANCE,  // broken or being repaired
    RETIRED             // no longer lent out
}

// The steps a reservation goes through (see the state diagram).
public enum ReservationStatus
{
    BOOKED,     // reserved, not picked up yet
    ISSUED,     // picked up by the borrower
    OVERDUE,    // picked up but not returned on time
    RETURNED,   // brought back
    CANCELLED,  // cancelled before pick-up
    EXPIRED     // never picked up
}

// Turns enum values into friendly words for the screen.
public static class Labels
{
    public static string Of(EquipmentType type)
    {
        if (type == EquipmentType.PROJECTOR) return "Projector";
        if (type == EquipmentType.HDMI_CABLE) return "HDMI cable";
        if (type == EquipmentType.REMOTE_CONTROL) return "Remote";
        return "Other";
    }

    public static string Of(EquipmentStatus status)
    {
        if (status == EquipmentStatus.AVAILABLE) return "Available";
        if (status == EquipmentStatus.IN_USE) return "In use";
        if (status == EquipmentStatus.UNDER_MAINTENANCE) return "Maintenance";
        return "Retired";
    }

    // The status inside a sentence, for example "This item is under maintenance".
    public static string InSentence(EquipmentStatus status)
    {
        if (status == EquipmentStatus.AVAILABLE) return "available";
        if (status == EquipmentStatus.IN_USE) return "in use";
        if (status == EquipmentStatus.UNDER_MAINTENANCE) return "under maintenance";
        return "retired";
    }

    public static string Of(ReservationStatus status)
    {
        if (status == ReservationStatus.BOOKED) return "Booked";
        if (status == ReservationStatus.ISSUED) return "Issued";
        if (status == ReservationStatus.OVERDUE) return "Overdue";
        if (status == ReservationStatus.RETURNED) return "Returned";
        if (status == ReservationStatus.CANCELLED) return "Cancelled";
        return "Expired";
    }

    public static string Of(Role role)
    {
        if (role == Role.STUDENT) return "Student";
        if (role == Role.LECTURER) return "Lecturer";
        return "IT Administrator";
    }
}
