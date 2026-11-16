package rw.auca.lectrolend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import rw.auca.lectrolend.model.ReturnRecord;

import java.util.Optional;

public interface ReturnRecordRepository extends JpaRepository<ReturnRecord, Long> {
    Optional<ReturnRecord> findByReservation_ReservationId(Long reservationId);

    long countByLateReturnTrue();
}
