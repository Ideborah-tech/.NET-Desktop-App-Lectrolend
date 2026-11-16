package rw.auca.lectrolend.repository;

import jakarta.persistence.LockModeType;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Lock;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import rw.auca.lectrolend.model.Equipment;
import rw.auca.lectrolend.model.enums.EquipmentStatus;

import java.util.Optional;

public interface EquipmentRepository extends JpaRepository<Equipment, Long> {

    boolean existsByAssetTagIgnoreCase(String assetTag);

    /** Row lock so two simultaneous bookings for the same item are serialised (no double booking). */
    @Lock(LockModeType.PESSIMISTIC_WRITE)
    @Query("select e from Equipment e where e.equipmentId = :id")
    Optional<Equipment> findByIdForUpdate(@Param("id") Long id);

    long countByStatus(EquipmentStatus status);

}
