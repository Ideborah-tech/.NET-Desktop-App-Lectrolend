package rw.auca.lectrolend.dto.response;

import java.time.LocalDateTime;
import rw.auca.lectrolend.model.enums.ReservationStatus;

public record ReservationDto(Long id, BorrowerInfo borrower, EquipmentInfo equipment, RoomInfo room,
                             LocalDateTime createdAt, LocalDateTime startTime, LocalDateTime endTime,
                             ReservationStatus status, LocalDateTime issuedAt, boolean overdue,
                             ReturnInfo returnInfo) {}
