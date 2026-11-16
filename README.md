# LectroLend: AUCA Equipment Management System

Spring Boot + PostgreSQL + React implementation of the LectroLend design (Assignment 1 plan, Assignment 2 ERD/UML).
Students and lecturers reserve projectors, HDMI cables and remotes; the IT administrator issues them at pick-up and
confirms returns.

> **Desktop version:** a Windows **.NET WinForms** implementation (same features, same database, a plain picture instead
> of the 3D scene) lives in [`desktop/`](desktop/README.md). Run it with `dotnet run --project desktop/src/LectroLend.Desktop`.

## Run it in VS Code

1. **File > Open Folder...** and open this `lectrolend` folder (the one containing `backend`, `frontend` and `.vscode`).
2. Accept the "install recommended extensions" prompt (Extension Pack for Java, Spring Boot Extension Pack). Wait for
   the Java project import to finish (bottom-right status).
3. Create the database once, in pgAdmin or `psql`: `CREATE DATABASE lectrolend;`
4. **Run with Maven (recommended):** press Ctrl+Shift+B (or Terminal > Run Build Task) to start
   **Run LectroLend (Maven API + Web)**. It runs `mvn spring-boot:run` for the backend and `npm run dev` for the
   frontend in two terminal panels, and asks for your PostgreSQL password once.
   Alternatively, open **Run and Debug** (Ctrl+Shift+D), choose **LectroLend: API + Web**, press **F5**, and type your PostgreSQL
   password when asked. The API starts on :8081 and the React app on http://localhost:5173.

If your JDK 21 is installed elsewhere, update the two paths in `.vscode/settings.json`.

## Run it from a terminal

You need: JDK 21 (a JDK 17+ also works if you change `java.version` in `backend/pom.xml`), Maven, Node 18+, PostgreSQL.
Your default `java` may be an old version; `scripts/start-backend.ps1` picks a `jdk-2x` folder automatically.

```powershell
.\scripts\start-backend.ps1     # terminal 1: asks for your PostgreSQL password, creates the DB, runs the API on :8081
.\scripts\start-frontend.ps1    # terminal 2: React app on http://localhost:5173
```

Open http://localhost:5173. Manual setup instead: create a database called `lectrolend`, then put your PostgreSQL
password in `backend/src/main/resources/application.properties` (`spring.datasource.password`) or set `DB_PASSWORD`
(other overridable values: `DB_HOST`, `DB_PORT`, `DB_NAME`, `DB_USER`, `SERVER_PORT`, `JWT_SECRET`) and run `mvn spring-boot:run`
in `backend/` and `npm install && npm run dev` in `frontend/`. If you change `SERVER_PORT`, set
`VITE_PROXY_TARGET` for the frontend to match.

### Demo accounts (created on first start)

| Role | Email | Password |
|------|-------|----------|
| IT Administrator | admin@auca.ac.rw | Admin@123 |
| Student | student@auca.ac.rw | Student@123 |
| Lecturer | lecturer@auca.ac.rw | Lecturer@123 |

Change or deactivate these before any real deployment, and set a strong `JWT_SECRET`.

## Project structure

```
lectrolend/
├── backend/                         Spring Boot 3 (Maven, Java 21)
│   ├── pom.xml
│   └── src/
│       ├── main/java/rw/auca/lectrolend/
│       │   ├── LectroLendApplication.java
│       │   ├── config/              SecurityConfig, DataSeeder (demo data)
│       │   ├── security/            JWT service, request filter, AuthUser principal
│       │   ├── controller/          REST endpoints (Auth, Equipment, Room, Reservation, User, Report)
│       │   ├── service/             business rules (reservations, equipment, users, reports)
│       │   ├── scheduler/           ReservationScheduler (expire / overdue, every minute)
│       │   ├── repository/          Spring Data JPA repositories
│       │   ├── model/               JPA entities: User > Borrower > Student/Lecturer, Admin, Equipment, ...
│       │   │   └── enums/           Role, EquipmentType, EquipmentStatus, ReservationStatus
│       │   ├── dto/
│       │   │   ├── request/         validated input records
│       │   │   └── response/        output records
│       │   └── exception/           custom exceptions + GlobalExceptionHandler
│       ├── main/resources/
│       │   ├── application.properties
│       │   └── db/migration/        V1__init_schema.sql (Flyway)
│       └── test/java/               unit tests
├── frontend/                        React 18 + Vite
│   └── src/
│       ├── main.jsx, App.jsx        entry point and routes
│       ├── api/                     http.js (fetch wrapper, token) + index.js (endpoint functions)
│       ├── context/                 AuthContext, ToastContext
│       ├── components/
│       │   ├── layout/              Layout (floating sidebar + canvas)
│       │   ├── ui/                  Modal, Badge, Field, PageHeader, Loading, Empty
│       │   ├── icons/               equipment icons
│       │   └── three/               Scene3D (sign-in / sign-up only)
│       ├── pages/
│       │   ├── auth/                login / register
│       │   ├── profile/             profile page (details, password)
│       │   ├── borrower/            catalogue, my reservations
│       │   └── admin/               dashboard, reservations, equipment, rooms, people
│       ├── utils/                   date and label formatting
│       └── styles/                  global.css (Dock theme)
├── scripts/                         start-backend.ps1, start-frontend.ps1
└── .vscode/                         tasks, launch configs, Java settings
```

## How the design maps to the code

| Design artefact | Implementation |
|---|---|
| ERD | `backend/src/main/resources/db/migration/V1__init_schema.sql` (Flyway); `users` + `student`/`lecturer`/`admin` joined tables |
| Class diagram | `model/`: abstract `User` -> `Borrower` -> `Student`/`Lecturer`, `Admin`; `Equipment`, `Room`, `Reservation`, `ReturnRecord`; `model/enums/`: `Role`, `EquipmentType`, `EquipmentStatus`, `ReservationStatus` |
| MVC layers | `controller/` (REST) -> `service/` (rules) -> `repository/` (Spring Data JPA) -> PostgreSQL; React is the view |
| Sequence diagram (make reservation) | `ReservationService.create`: validate, lock equipment, `isReservable`, overlap check, save as BOOKED; conflicts return **409** |
| State diagram | `Reservation.cancel/markIssued/markReturned/expire/markOverdue`; `scheduler/ReservationScheduler` expires bookings not collected within 30 min and flags OVERDUE |
| Activity diagram | Borrower reserves, admin **issues** (equipment -> IN_USE), admin **receives return** (damaged -> UNDER_MAINTENANCE, else AVAILABLE; late flagged) |
| Use cases | Borrower: catalogue, availability, reserve (+optional room), cancel, my reservations. Admin: equipment CRUD + status, rooms, accounts, issue, return, reports |
| Security | JWT login, BCrypt hashes, role checks (`@PreAuthorize`), deactivated accounts lose access immediately |

Decisions beyond the diagrams: students may hold 3 active reservations and lecturers 5; a reservation is at most 7 days;
pick-up opens 30 minutes before the start time; an item that is IN_USE can still be booked for a later slot, while
maintenance/retired items cannot; equipment with history is retired instead of deleted. Double booking is prevented by a
row lock on the equipment inside the reservation transaction.

## API (all under `/api`, JWT bearer token except login/register)

| Method & path | Who |
|---|---|
| `POST /auth/login`, `POST /auth/register` (students and lecturers only), `GET /auth/me` | public / any |
| `PUT /auth/me` (edit your profile), `POST /auth/change-password` | any user |
| `GET /equipment`, `GET /equipment/{id}/availability?start=&end=` | any user |
| `POST/PUT/DELETE /equipment`, `PATCH /equipment/{id}/status` | admin |
| `GET /rooms`; `POST/PUT/DELETE /rooms` | any / admin |
| `POST /reservations`, `POST /reservations/batch` (several items, all-or-nothing), `GET /reservations/mine` | student, lecturer / any |
| `POST /reservations/{id}/cancel` | owner or admin |
| `GET /reservations`, `POST /reservations/{id}/issue`, `POST /reservations/{id}/return` | admin |
| `GET/POST /users`, `PATCH /users/{id}/active`, `GET /reports/summary` | admin |

## Tests

`mvn test` in `backend/` runs the reservation state-machine unit tests. The full API flow (auth, role checks, double
booking, limits, issue/return, maintenance) was also exercised end to end against a real PostgreSQL instance.

## Design

"Dock". Brand colours (kept in `frontend/src/styles/global.css` as `--navy` and `--accent`):

| Name | Value | Java equivalent |
|---|---|---|
| Primary blue | `#03346E` | `new Color(3, 52, 110)` |
| Accent blue | `#2C74B3` | `new Color(44, 116, 179)` |

plus a sunny yellow (`#FFC93C`) for highlights. Everything is a **solid colour** (no gradients) with rounded shapes: pills, circles
and large radii. Fredoka (rounded) for headings, Manrope for text, DM Mono for small labels (bundled through `@fontsource`).

- **Dock:** a slim header (wordmark, your profile, Sign out) and a floating pill **dock** at the bottom of the screen for navigation.
- **Sign in / Create account:** a **3D scene** (a floating projector, remote and HDMI cable in the brand colours, built with
  three.js) filling a navy page beside a tall rounded white sheet that holds the form. The two **swap sides** between sign-in and sign-up and neither
  page scrolls on a laptop screen. three.js is loaded only on these two pages, paused when off screen, and skipped on phones or
  without WebGL. There are no numbered points anywhere.
- **Borrow page:** a search box and type pills, then **horizontal shelves** per equipment type, with a floating **booking pill** for what you already hold.
- **Reserve dialog:** a two-panel dialog (item and rules on the left; dates, quick durations, live availability, a one-day
  timeline and an optional room on the right).
- **Dashboard:** a navy hero with a big yellow *out now* circle and a stat strip, two work lanes (To collect with Hand over, Out now with Check in), an equipment ring, popular items and latest bookings.
