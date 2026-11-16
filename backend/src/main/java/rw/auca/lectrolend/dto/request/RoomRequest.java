package rw.auca.lectrolend.dto.request;

import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;

public record RoomRequest(
        @NotBlank(message = "Room code is required") @Size(max = 20) String roomCode,
        @NotBlank(message = "Building is required") @Size(max = 60) String building,
        @Min(value = 1, message = "Capacity must be at least 1") int capacity) {}
