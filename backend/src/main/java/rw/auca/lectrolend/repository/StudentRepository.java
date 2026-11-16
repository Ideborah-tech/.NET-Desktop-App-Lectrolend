package rw.auca.lectrolend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import rw.auca.lectrolend.model.Student;

public interface StudentRepository extends JpaRepository<Student, Long> {
    boolean existsByRegistrationNo(String registrationNo);
}
