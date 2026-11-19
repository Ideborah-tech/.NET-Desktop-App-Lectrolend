using LectroLend.Core.Data;
using LectroLend.Core.Services;

namespace LectroLend.Desktop.Controllers;

// MVC: the Views (windows) only talk to these Controllers.
//      The Controllers talk to the Model (LectroLend.Core: the classes, the rules and the database).
//
//      View (Form / Page)  --->  Controller  --->  Model (services, classes, database)
//      View (Form / Page)  <---  Controller  <---  Model
//
// This object holds the four controllers. Every window receives it.
public class AppControllers
{
    public AccountController Account;
    public EquipmentController Equipment;
    public RoomController Rooms;
    public ReservationController Reservations;

    public AppControllers(DbSettings settings)
    {
        // The model services (they read and write the database).
        UserService userService = new UserService(settings);
        AuthService authService = new AuthService(settings, userService);
        EquipmentService equipmentService = new EquipmentService(settings);
        RoomService roomService = new RoomService(settings);
        ReservationService reservationService = new ReservationService(settings);
        ReportService reportService = new ReportService(settings);

        // The controllers that the windows use.
        Account = new AccountController(authService, userService);
        Equipment = new EquipmentController(equipmentService);
        Rooms = new RoomController(roomService);
        Reservations = new ReservationController(reservationService, reportService);
    }
}
