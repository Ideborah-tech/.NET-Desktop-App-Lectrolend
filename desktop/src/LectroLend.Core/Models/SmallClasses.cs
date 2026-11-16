namespace LectroLend.Core.Models;

// Everything typed in the "Create account" form.
public class RegisterRequest
{
    public Role Role { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string RegistrationNo { get; set; }  // students only
    public string Faculty { get; set; }         // students only
    public string Program { get; set; }         // students only
    public string StaffNo { get; set; }         // lecturers and admins
    public string Department { get; set; }      // lecturers only
}

// A start time and an end time, for example one booking of an item.
public class TimeSlot
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

// The answer to "Is this item free at that time?"
public class AvailabilityResult
{
    public bool Available { get; set; }
    public string Reason { get; set; }                     // why it is not free (empty when free)
    public List<TimeSlot> BookedTimes { get; set; } = new List<TimeSlot>();
}

// The numbers shown at the top of the dashboard.
public class DashboardNumbers
{
    public int TotalEquipment { get; set; }
    public int AvailableEquipment { get; set; }
    public int LateReturns { get; set; }
    public int TotalUsers { get; set; }
}
