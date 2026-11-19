# LectroLend: AUCA Equipment Lending (Windows desktop app)

LectroLend lets students and lecturers of the **Adventist University of Central Africa (AUCA)** book projectors, HDMI
cables, remotes and other equipment from the IT office. The IT office hands the items over and checks them back in.

Built with **C# .NET 8 WinForms + Entity Framework Core + PostgreSQL**, following the **MVC** architecture.
Everything is done through the windows (GUI): signing in, booking, handing over, checking in, and managing
equipment, rooms and accounts. All data is saved in a PostgreSQL database.

The code is written for beginners: plain classes and `if / else`, one class per window, named click handlers such as
`SaveButton_Click`, `try / catch` around every database call, and short comments in simple English. Nullable
reference types are turned off, so there are no `?` and `!` marks on normal types. Rule errors are thrown as
`AppException` with a message that the window shows to the user.

The sign-in page shows the AUCA logo (`Assets/auca-logo.png`, also in the main window's top bar) and a welcome
picture (`Assets/welcome.jpg`). To change a picture, replace the file and keep the same file name.

## MVC architecture

The app follows **Model - View - Controller**:

```
 VIEW (windows)        --->  CONTROLLER              --->  MODEL
 Views/Forms, Pages          Controllers/                  LectroLend.Core (Models, Services, Data)
 shows data, reacts          decides what to do,           the classes, the rules
 to clicks                   calls the model               and the PostgreSQL database
```

| Part | Folder | What it does |
|---|---|---|
| **Model** | `src/LectroLend.Core` | `Models/` (User, Student, Lecturer, Admin, Equipment, Room, Reservation, ReturnRecord), `Services/` (the business rules), `Data/` (EF Core + PostgreSQL). Knows nothing about windows. |
| **View** | `src/LectroLend.Desktop/Views` | `Forms/` (windows), `Pages/` (screens inside the main window), `UI/` (colours and custom controls). Views only call controllers. |
| **Controller** | `src/LectroLend.Desktop/Controllers` | `AccountController`, `EquipmentController`, `RoomController`, `ReservationController`, plus `Session` (who is signed in). |

Example: clicking **Reserve** runs `ReserveButton_Click` in the view, which calls
`app.Reservations.Reserve(item, start, end, room)` in the controller, which calls `ReservationService.Create(...)`
in the model, which checks the rules and saves the booking in the database.

## Run it

You need the **.NET 8 SDK** (or newer) and **PostgreSQL** running locally.

```powershell
dotnet run --project src/LectroLend.Desktop
```

Or open `LectroLend.Desktop.sln` in Visual Studio (2022 or newer) and press **F5**.

## Change the windows in Design view (Visual Studio)

Every window and page has the normal Visual Studio files:

| File | What it holds |
|---|---|
| `LoginForm.cs` | Your code: filling in values and what happens on a click (`SignInButton_Click`). |
| `LoginForm.Designer.cs` | The controls, their position, size, colour and font. Written by the designer; do not edit by hand. |
| `LoginForm.resx` | Resources that Visual Studio stores for the window. |

1. Open `LectroLend.Desktop.sln` in Visual Studio and **build once** (Ctrl+Shift+B). The designer needs the
   build to show the custom controls (`RoundButton`, `CardPanel`, `InputBox`).
2. In Solution Explorer, **double-click** a window, for example `Views/Forms/LoginForm.cs`. Design view opens.
3. Click a control, then change it in the **Properties** window (F4), or drag it with the mouse.
4. Double-click a button to jump to its click method in the `.cs` file.

After a build, `RoundButton`, `CardPanel` and `InputBox` also appear at the top of the **Toolbox**, ready to drag
onto a form. For an `InputBox`, set `Hint` and `IsPassword` in the Properties window.

A few things are made in code while the app runs, because they depend on the data or on who signed in, so Design view
shows only their empty area: the equipment cards (Borrow page), the rows of every table, and the menu buttons
(main window). The pictures are also loaded when the app runs, from `Assets/welcome.jpg` and `Assets/auca-logo.png`.

The first time, a **Connect to PostgreSQL** window asks for your PostgreSQL password. The app then creates the
`lectrolend` database (if missing), builds the tables and loads the demo data. The connection is saved for your Windows
account in `%AppData%\LectroLend\connection.json` (delete that file to be asked again).

To build a single folder you can copy to another PC:

```powershell
dotnet publish src/LectroLend.Desktop -c Release -r win-x64 --self-contained -o publish
```

### Demo accounts

| Role | Email | Password |
|------|-------|----------|
| IT Administrator | admin@auca.ac.rw | Admin@123 |
| Student | student@auca.ac.rw | Student@123 |
| Lecturer | lecturer@auca.ac.rw | Lecturer@123 |

The sign-in screen has three demo buttons (Student, Lecturer, IT admin): one click fills in the email and password
and signs in. Remove the buttons (`LoginForm`) and change the passwords before any real deployment.

## Project structure

```
lectrolend/
├── LectroLend.Desktop.sln
├── src/
│   ├── LectroLend.Core/               class library: model + data + business rules (no UI)
│   │   ├── Models/                    User (abstract) -> Student / Lecturer (IBorrower), Admin; Equipment, Room,
│   │   │                              Reservation (state machine), ReturnRecord; enums; AppException
│   │   ├── Data/                      LectroLendDbContext (EF Core; tables and columns set with [Table] / [Column]),
│   │   │                              Schema.sql (creates the tables), DatabaseInitializer (create + seed),
│   │   │                              DbSettings, BCrypt Passwords
│   │   └── Services/                  AuthService, UserService, EquipmentService, RoomService,
│   │                                  ReservationService, ReportService, Check (input checks)
│   └── LectroLend.Desktop/            WinForms app (Views + Controllers)
│       ├── Program.cs                 connect -> sign in -> main window (loops back after Sign out)
│       ├── Controllers/               AppControllers, Session, AccountController, EquipmentController,
│       │                              RoomController, ReservationController
│       ├── Views/
│       │   ├── Forms/                 windows (each = .cs + .Designer.cs + .resx): ConnectionForm, LoginForm,
│       │   │                          RegisterForm, MainForm, ReserveForm, ReturnForm, EquipmentForm, RoomForm
│       │   ├── Pages/                 screens in the main window (UserControls): BorrowPage, MyReservationsPage,
│       │   │                          ProfilePage, DashboardPage, ReservationsPage, EquipmentPage, RoomsPage, PeoplePage
│       │   └── UI/                    Ui.cs (colours, fonts, StyleGrid, messages); RoundButton, CardPanel, InputBox
│       └── Assets/                    auca-logo.png (university logo), welcome.jpg (sign-in picture), app.ico
└── tests/LectroLend.Core.Tests/       xUnit: state machine, booking rules, EF mapping vs the SQL schema
```

## How the design maps to the code

| Design artefact | Desktop implementation |
|---|---|
| ERD | `Data/Schema.sql` + `LectroLendDbContext` (TPT: `users` + `student` / `lecturer` / `admin`) |
| Class diagram | `Models/`: abstract `User`; `Student` and `Lecturer` implement `IBorrower` (the Borrower generalisation, limits 3 and 5); `Admin` |
| MVC layers | Views (Forms/Pages) -> Controllers -> Model (Services -> EF Core DbContext -> PostgreSQL) |
| Sequence diagram (make reservation) | `ReservationService.Create`: validate window, check limit, `SELECT ... FOR UPDATE` on each item, `IsReservable`, overlap check, save as BOOKED, all in one transaction |
| State diagram | `Reservation.Cancel / MarkIssued / MarkReturned / Expire / MarkOverdue`; `MainForm` runs `RunHousekeeping` every minute (expire after 30 min, flag OVERDUE) |
| Activity diagram | Borrower reserves; admin **Hand over** (item -> IN_USE); admin **Check in** (damaged -> UNDER_MAINTENANCE, else AVAILABLE; late returns flagged) |
| Use cases | Borrower: browse and search, check availability, reserve an item (+ optional room), cancel, my reservations, profile. Admin: dashboard, issue, return, cancel, CSV export, equipment, rooms, accounts |

## Tests

```powershell
dotnet test
```

The tests need no database. The full service flow (seeding, booking, double booking, limits, issue/return, housekeeping,
reports, equipment / room / account management) was also run against a real PostgreSQL 16 instance.
