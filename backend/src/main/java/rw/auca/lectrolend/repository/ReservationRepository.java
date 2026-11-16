package rw.auca.lectrolend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import rw.auca.lectrolend.model.Reservation;
import rw.auca.lectrolend.model.enums.ReservationStatus;

import java.time.LocalDateTime;
import java.util.Collection;
import java.util.List;

public interface ReservationRepository extends JpaRepository<Reservation, Long> {

    @Query("""
            select count(r) from Reservation r
            where r.equipment.equipmentId = :equipmentId
              and r.status in :statuses
              and r.startTime < :end and r.endTime > :start
            """)
    long countOverlaps(@Param("equipmentId") Long equipmentId,
                       @Param("start") LocalDateTime start,
                       @Param("end") LocalDateTime end,
                       @Param("statuses") Collection<ReservationStatus> statuses);

    @Query("""
            select r from Reservation r
            where r.equipment.equipmentId = :equipmentId
              and r.status in :statuses
              and r.endTime > :from
            order by r.startTime
            """)
    List<Reservation> findUpcoming(@Param("equipmentId") Long equipmentId,
                                   @Param("from") LocalDateTime from,
                                   @Param("statuses") Collection<ReservationStatus> statuses);

    long countByUser_UserIdAndStatusIn(Long userId, Collection<ReservationStatus> statuses);

    boolean existsByEquipment_EquipmentId(Long equipmentId);

    boolean existsByEquipment_EquipmentIdAndStatusIn(Long equipmentId, Collection<ReservationStatus> statuses);

    List<Reservation> findByUser_UserIdOrderByStartTimeDesc(Long userId);

    List<Reservation> findAllByOrderByStartTimeDesc();

    List<Reservation> findByStatusOrderByStartTimeDesc(ReservationStatus status);

    List<Reservation> findByStatusAndStartTimeBefore(ReservationStatus status, LocalDateTime cutoff);

    List<Reservation> findByStatusAndEndTimeBefore(ReservationStatus status, LocalDateTime cutoff);

    long countByStatus(ReservationStatus status);

    @Query("""
            select r.equipment.name, count(r) from Reservation r
            group by r.equipment.name order by count(r) desc
            """)
    List<Object[]> mostBorrowed();
}
