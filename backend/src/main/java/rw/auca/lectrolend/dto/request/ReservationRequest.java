package rw.auca.lectrolend.dto.request;

import jakarta.validation.constraints.NotNull;
import java.time.LocalDateTime;

public record ReservationRequest(
        @NotNull(message = "Choose an equipment item") Long equipmentId,
        @NotNull(message = "Start time is required") LocalDateTime startTime,
        @NotNull(message = "End time is required") LocalDateTime endTime,
        Long roomId) {}
