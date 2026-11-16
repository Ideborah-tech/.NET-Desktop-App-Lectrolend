package rw.auca.lectrolend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import rw.auca.lectrolend.model.Admin;

public interface AdminRepository extends JpaRepository<Admin, Long> {
    boolean existsByStaffNo(String staffNo);
}
