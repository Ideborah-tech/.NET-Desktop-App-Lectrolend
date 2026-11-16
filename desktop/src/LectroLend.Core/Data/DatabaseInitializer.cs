using LectroLend.Core.Models;
using Npgsql;

namespace LectroLend.Core.Data;

// Gets the database ready the first time the app starts:
//   1. creates the "lectrolend" database if it does not exist,
//   2. creates the tables (from Schema.sql) if they do not exist,
//   3. adds demo users, rooms and equipment if there are no users yet.
public static class DatabaseInitializer
{
    public static void Initialize(DbSettings settings)
    {
        CreateDatabaseIfMissing(settings);
        CreateTablesIfMissing(settings);
        AddDemoDataIfEmpty(settings);
    }

    private static void CreateDatabaseIfMissing(DbSettings settings)
    {
        // The name goes straight into SQL, so we only allow letters, numbers and "_".
        foreach (char c in settings.Database)
        {
            if (!char.IsLetterOrDigit(c) && c != '_')
                throw new Exception("The database name may only contain letters, numbers and _");
        }

        // Connect to the built-in "postgres" database to check if ours exists.
        using (NpgsqlConnection connection = new NpgsqlConnection(settings.ConnectionString("postgres")))
        {
            connection.Open();

            NpgsqlCommand check = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = @name", connection);
            check.Parameters.AddWithValue("name", settings.Database);
            bool exists = check.ExecuteScalar() != null;

            if (!exists)
            {
                NpgsqlCommand create = new NpgsqlCommand("CREATE DATABASE \"" + settings.Database + "\"", connection);
                create.ExecuteNonQuery();
            }
        }
    }

    private static void CreateTablesIfMissing(DbSettings settings)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(settings.ConnectionString()))
        {
            connection.Open();

            // If the "users" table exists, the tables were already created.
            NpgsqlCommand check = new NpgsqlCommand("SELECT to_regclass('public.users') IS NOT NULL", connection);
            bool tablesExist = (bool)check.ExecuteScalar();
            if (tablesExist)
                return;

            // Schema.sql is stored inside the program (see LectroLend.Core.csproj).
            string sql;
            using (Stream file = typeof(DatabaseInitializer).Assembly.GetManifestResourceStream("LectroLend.Core.Schema.sql"))
            using (StreamReader reader = new StreamReader(file))
            {
                sql = reader.ReadToEnd();
            }

            NpgsqlCommand create = new NpgsqlCommand(sql, connection);
            create.ExecuteNonQuery();
        }
    }

    private static void AddDemoDataIfEmpty(DbSettings settings)
    {
        using (LectroLendDbContext db = new LectroLendDbContext(settings))
        {
            if (db.Users.Any())
                return; // there is already data

            // ---- demo accounts ----
            Admin admin = new Admin();
            admin.FullName = "IT Administrator";
            admin.Email = "admin@auca.ac.rw";
            admin.Phone = "+250788000001";
            admin.StaffNo = "ITA-001";
            admin.PasswordHash = Passwords.Hash("Admin@123");
            db.Admins.Add(admin);

            Student student = new Student();
            student.FullName = "Ingabire Deborah";
            student.Email = "student@auca.ac.rw";
            student.Phone = "+250788000002";
            student.RegistrationNo = "26844";
            student.Faculty = "Information Technology";
            student.Program = "Software Engineering";
            student.PasswordHash = Passwords.Hash("Student@123");
            db.Students.Add(student);

            Lecturer lecturer = new Lecturer();
            lecturer.FullName = "Dr. Jean Claude Uwimana";
            lecturer.Email = "lecturer@auca.ac.rw";
            lecturer.Phone = "+250788000003";
            lecturer.StaffNo = "LEC-014";
            lecturer.Department = "Computer Science";
            lecturer.PasswordHash = Passwords.Hash("Lecturer@123");
            db.Lecturers.Add(lecturer);

            // ---- rooms ----
            AddRoom(db, "A101", "Main Building", 60);
            AddRoom(db, "A102", "Main Building", 40);
            AddRoom(db, "B204", "Science Block", 30);
            AddRoom(db, "LAB1", "IT Block", 25);
            AddRoom(db, "LAB2", "IT Block", 25);
            AddRoom(db, "CHAPEL", "Central Campus", 300);

            // ---- equipment ----
            AddItem(db, admin, "Epson EB-X51 Projector", EquipmentType.PROJECTOR, "PRJ-001", "3800 lumens, XGA. Includes power cable.");
            AddItem(db, admin, "Epson EB-X51 Projector", EquipmentType.PROJECTOR, "PRJ-002", "3800 lumens, XGA. Includes power cable.");
            AddItem(db, admin, "BenQ MH535 Projector", EquipmentType.PROJECTOR, "PRJ-003", "Full HD, 3600 lumens. Carry case included.");
            AddItem(db, admin, "HDMI Cable 5 m", EquipmentType.HDMI_CABLE, "HDMI-001", "High-speed HDMI 2.0, 5 metres.");
            AddItem(db, admin, "HDMI Cable 5 m", EquipmentType.HDMI_CABLE, "HDMI-002", "High-speed HDMI 2.0, 5 metres.");
            AddItem(db, admin, "HDMI Cable 10 m", EquipmentType.HDMI_CABLE, "HDMI-003", "For large halls, 10 metres.");
            AddItem(db, admin, "Projector Remote (Epson)", EquipmentType.REMOTE_CONTROL, "RMT-001", "Batteries included.");
            AddItem(db, admin, "Projector Remote (BenQ)", EquipmentType.REMOTE_CONTROL, "RMT-002", "Batteries included.");
            AddItem(db, admin, "Wireless Presenter", EquipmentType.REMOTE_CONTROL, "RMT-003", "USB receiver, laser pointer, slide control.");
            AddItem(db, admin, "HDMI to VGA Adapter", EquipmentType.OTHER, "ADP-001", "For older lecture-room projectors.");
            AddItem(db, admin, "Portable Speaker", EquipmentType.OTHER, "SPK-001", "Bluetooth, 20 W, with 3.5 mm cable.");
            AddItem(db, admin, "Extension Cord 5 m", EquipmentType.OTHER, "EXT-001", "4-way power strip.");

            db.SaveChanges(); // write everything to the database
        }
    }

    private static void AddRoom(LectroLendDbContext db, string code, string building, int capacity)
    {
        Room room = new Room();
        room.RoomCode = code;
        room.Building = building;
        room.Capacity = capacity;
        db.Rooms.Add(room);
    }

    private static void AddItem(LectroLendDbContext db, Admin admin, string name, EquipmentType type, string tag, string description)
    {
        Equipment item = new Equipment();
        item.AddedBy = admin;
        item.Name = name;
        item.Type = type;
        item.AssetTag = tag;
        item.Description = description;
        item.Status = EquipmentStatus.AVAILABLE;
        db.Equipment.Add(item);
    }
}
