package rw.auca.lectrolend.dto.request;

import jakarta.validation.constraints.NotNull;
import rw.auca.lectrolend.model.enums.EquipmentStatus;

public record StatusRequest(@NotNull(message = "Status is required") EquipmentStatus status) {}
