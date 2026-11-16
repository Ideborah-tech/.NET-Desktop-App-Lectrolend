package rw.auca.lectrolend.service;

import lombok.RequiredArgsConstructor;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import rw.auca.lectrolend.dto.request.ChangePasswordRequest;
import rw.auca.lectrolend.dto.request.RegisterRequest;
import rw.auca.lectrolend.dto.request.UpdateProfileRequest;
import rw.auca.lectrolend.dto.response.UserDto;
import rw.auca.lectrolend.exception.BadRequestException;
import rw.auca.lectrolend.exception.NotFoundException;
import rw.auca.lectrolend.model.Admin;
import rw.auca.lectrolend.model.Lecturer;
import rw.auca.lectrolend.model.Student;
import rw.auca.lectrolend.model.User;
import rw.auca.lectrolend.model.enums.Role;
import rw.auca.lectrolend.repository.*;

import java.util.List;

@Service
@RequiredArgsConstructor
public class UserService {

    private final UserRepository users;
    private final StudentRepository students;
    private final LecturerRepository lecturers;
    private final AdminRepository admins;
    private final PasswordEncoder encoder;

    public static UserDto toDto(User u) {
        String reg = null, faculty = null, program = null, staffNo = null, dept = null;
        if (u instanceof Student s) {
            reg = s.getRegistrationNo(); faculty = s.getFaculty(); program = s.getProgram();
        } else if (u instanceof Lecturer l) {
            staffNo = l.getStaffNo(); dept = l.getDepartment();
        } else if (u instanceof Admin a) {
            staffNo = a.getStaffNo();
        }
        return new UserDto(u.getUserId(), u.getFullName(), u.getEmail(), u.getPhone(), u.getRole(), u.isActive(),
                reg, faculty, program, staffNo, dept);
    }

    @Transactional
    public User create(RegisterRequest req, boolean allowAdminRole) {
        if (req.role() == Role.ADMIN && !allowAdminRole) {
            throw new BadRequestException("Only an administrator can create administrator accounts");
        }
        String email = req.email().trim().toLowerCase();
        if (users.existsByEmailIgnoreCase(email)) {
            throw new BadRequestException("An account with this email already exists");
        }
        User user = switch (req.role()) {
            case STUDENT -> {
                String reg = required(req.registrationNo(), "Registration number is required for students");
                if (students.existsByRegistrationNo(reg)) throw new BadRequestException("Registration number already in use");
                Student s = new Student();
                s.setRegistrationNo(reg);
                s.setFaculty(blankToNull(req.faculty()));
                s.setProgram(blankToNull(req.program()));
                yield s;
            }
            case LECTURER -> {
                String staff = required(req.staffNo(), "Staff number is required for lecturers");
                if (lecturers.existsByStaffNo(staff)) throw new BadRequestException("Staff number already in use");
                Lecturer l = new Lecturer();
                l.setStaffNo(staff);
                l.setDepartment(blankToNull(req.department()));
                yield l;
            }
            case ADMIN -> {
                String staff = required(req.staffNo(), "Staff number is required for administrators");
                if (admins.existsByStaffNo(staff)) throw new BadRequestException("Staff number already in use");
                Admin a = new Admin();
                a.setStaffNo(staff);
                yield a;
            }
        };
        user.setFullName(req.fullName().trim());
        user.setEmail(email);
        user.setPhone(blankToNull(req.phone()));
        user.setPasswordHash(encoder.encode(req.password()));
        return users.save(user);
    }

    @Transactional(readOnly = true)
    public List<UserDto> list() {
        return users.findAll().stream().map(UserService::toDto).toList();
    }

    @Transactional(readOnly = true)
    public UserDto get(Long id) {
        return toDto(find(id));
    }

    @Transactional
    public UserDto setActive(Long id, boolean active, Long actingAdminId) {
        if (id.equals(actingAdminId) && !active) {
            throw new BadRequestException("You cannot deactivate your own account");
        }
        User u = find(id);
        u.setActive(active);
        return toDto(u);
    }

    /** Lets a user edit their own name, phone and the profile fields that belong to their role. */
    @Transactional
    public UserDto updateProfile(Long id, UpdateProfileRequest req) {
        User u = find(id);
        u.updateProfile(req.fullName().trim(), blankToNull(req.phone()));
        if (u instanceof Student s) {
            s.setFaculty(blankToNull(req.faculty()));
            s.setProgram(blankToNull(req.program()));
        } else if (u instanceof Lecturer l) {
            l.setDepartment(blankToNull(req.department()));
        }
        return toDto(u);
    }

    @Transactional
    public void changePassword(Long id, ChangePasswordRequest req) {
        User u = find(id);
        if (!encoder.matches(req.currentPassword(), u.getPasswordHash())) {
            throw new BadRequestException("Your current password is incorrect");
        }
        if (encoder.matches(req.newPassword(), u.getPasswordHash())) {
            throw new BadRequestException("Choose a new password that is different from the current one");
        }
        u.setPasswordHash(encoder.encode(req.newPassword()));
    }

    public User find(Long id) {
        return users.findById(id).orElseThrow(() -> new NotFoundException("User not found"));
    }

    private static String required(String v, String msg) {
        if (v == null || v.isBlank()) throw new BadRequestException(msg);
        return v.trim();
    }

    private static String blankToNull(String v) {
        return v == null || v.isBlank() ? null : v.trim();
    }
}
