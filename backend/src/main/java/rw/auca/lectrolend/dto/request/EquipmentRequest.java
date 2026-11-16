package rw.auca.lectrolend.dto.request;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;
import rw.auca.lectrolend.model.enums.EquipmentStatus;
import rw.auca.lectrolend.model.enums.EquipmentType;

public record EquipmentRequest(
        @NotBlank(message = "Name is required") @Size(max = 80) String name,
        @NotNull(message = "Type is required") EquipmentType type,
        @NotBlank(message = "Asset tag is required") @Size(max = 30) String assetTag,
        EquipmentStatus status,
        String description) {}
