package rw.auca.lectrolend.dto.request;

import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;
import java.time.LocalDateTime;
import java.util.List;

/** Several items for one time slot; one reservation is created per item. */
public record BatchReservationRequest(
        @NotEmpty(message = "Choose at least one item") @Size(max = 10, message = "Too many items at once") List<Long> equipmentIds,
        @NotNull(message = "Start time is required") LocalDateTime startTime,
        @NotNull(message = "End time is required") LocalDateTime endTime,
        Long roomId) {}
