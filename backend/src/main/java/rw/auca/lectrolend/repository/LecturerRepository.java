package rw.auca.lectrolend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import rw.auca.lectrolend.model.Lecturer;

public interface LecturerRepository extends JpaRepository<Lecturer, Long> {
    boolean existsByStaffNo(String staffNo);
}
