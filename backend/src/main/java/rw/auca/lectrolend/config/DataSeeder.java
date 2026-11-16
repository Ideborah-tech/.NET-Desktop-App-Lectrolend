package rw.auca.lectrolend.config;

import lombok.RequiredArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.boot.ApplicationArguments;
import org.springframework.boot.ApplicationRunner;
import org.springframework.boot.autoconfigure.condition.ConditionalOnProperty;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Component;
import org.springframework.transaction.annotation.Transactional;
import rw.auca.lectrolend.model.Admin;
import rw.auca.lectrolend.model.Equipment;
import rw.auca.lectrolend.model.Lecturer;
import rw.auca.lectrolend.model.Room;
import rw.auca.lectrolend.model.Student;
import rw.auca.lectrolend.model.enums.EquipmentStatus;
import rw.auca.lectrolend.model.enums.EquipmentType;
import rw.auca.lectrolend.repository.*;

/** Loads demo accounts, rooms and equipment on first start (the "load initial data" deployment step). */
@Component
@RequiredArgsConstructor
@ConditionalOnProperty(name = "lectrolend.seed.enabled", havingValue = "true")
public class DataSeeder implements ApplicationRunner {

    private static final Logger log = LoggerFactory.getLogger(DataSeeder.class);

    private final UserRepository users;
    private final AdminRepository admins;
    private final EquipmentRepository equipment;
    private final RoomRepository rooms;
    private final PasswordEncoder encoder;

    @Override
    @Transactional
    public void run(ApplicationArguments args) {
        if (users.count() > 0) return;

        Admin admin = new Admin();
        admin.setFullName("IT Administrator");
        admin.setEmail("admin@auca.ac.rw");
        admin.setPhone("+250788000001");
        admin.setStaffNo("ITA-001");
        admin.setPasswordHash(encoder.encode("Admin@123"));
        admins.save(admin);

        Student student = new Student();
        student.setFullName("Ingabire Deborah");
        student.setEmail("student@auca.ac.rw");
        student.setPhone("+250788000002");
        student.setRegistrationNo("26844");
        student.setFaculty("Information Technology");
        student.setProgram("Software Engineering");
        student.setPasswordHash(encoder.encode("Student@123"));
        users.save(student);

        Lecturer lecturer = new Lecturer();
        lecturer.setFullName("Dr. Jean Claude Uwimana");
        lecturer.setEmail("lecturer@auca.ac.rw");
        lecturer.setPhone("+250788000003");
        lecturer.setStaffNo("LEC-014");
        lecturer.setDepartment("Computer Science");
        lecturer.setPasswordHash(encoder.encode("Lecturer@123"));
        users.save(lecturer);

        room("A101", "Main Building", 60);
        room("A102", "Main Building", 40);
        room("B204", "Science Block", 30);
        room("LAB1", "IT Block", 25);
        room("LAB2", "IT Block", 25);
        room("CHAPEL", "Central Campus", 300);

        item(admin, "Epson EB-X51 Projector", EquipmentType.PROJECTOR, "PRJ-001", "3800 lumens, XGA. Includes power cable.");
        item(admin, "Epson EB-X51 Projector", EquipmentType.PROJECTOR, "PRJ-002", "3800 lumens, XGA. Includes power cable.");
        item(admin, "BenQ MH535 Projector", EquipmentType.PROJECTOR, "PRJ-003", "Full HD, 3600 lumens. Carry case included.");
        item(admin, "HDMI Cable 5 m", EquipmentType.HDMI_CABLE, "HDMI-001", "High-speed HDMI 2.0, 5 metres.");
        item(admin, "HDMI Cable 5 m", EquipmentType.HDMI_CABLE, "HDMI-002", "High-speed HDMI 2.0, 5 metres.");
        item(admin, "HDMI Cable 10 m", EquipmentType.HDMI_CABLE, "HDMI-003", "For large halls, 10 metres.");
        item(admin, "Projector Remote (Epson)", EquipmentType.REMOTE_CONTROL, "RMT-001", "Batteries included.");
        item(admin, "Projector Remote (BenQ)", EquipmentType.REMOTE_CONTROL, "RMT-002", "Batteries included.");
        item(admin, "Wireless Presenter", EquipmentType.REMOTE_CONTROL, "RMT-003", "USB receiver, laser pointer, slide control.");
        item(admin, "HDMI to VGA Adapter", EquipmentType.OTHER, "ADP-001", "For older lecture-room projectors.");
        item(admin, "Portable Speaker", EquipmentType.OTHER, "SPK-001", "Bluetooth, 20 W, with 3.5 mm cable.");
        item(admin, "Extension Cord 5 m", EquipmentType.OTHER, "EXT-001", "4-way power strip.");

        log.info("Seeded demo data: admin@auca.ac.rw / student@auca.ac.rw / lecturer@auca.ac.rw");
    }

    private void room(String code, String building, int capacity) {
        Room r = new Room();
        r.setRoomCode(code);
        r.setBuilding(building);
        r.setCapacity(capacity);
        rooms.save(r);
    }

    private void item(Admin by, String name, EquipmentType type, String tag, String desc) {
        Equipment e = new Equipment();
        e.setAddedBy(by);
        e.setName(name);
        e.setType(type);
        e.setAssetTag(tag);
        e.setDescription(desc);
        e.setStatus(EquipmentStatus.AVAILABLE);
        equipment.save(e);
    }
}
