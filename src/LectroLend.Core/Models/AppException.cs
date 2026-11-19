namespace LectroLend.Core.Models;

// An error we want to show to the user, for example "This item is already booked for that time."
// The services "throw" it when a rule is broken, and the windows "catch" it and show the message.
public class AppException : Exception
{
    public AppException(string message) : base(message)
    {
    }
}
