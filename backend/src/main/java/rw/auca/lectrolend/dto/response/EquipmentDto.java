package rw.auca.lectrolend.dto.response;

import rw.auca.lectrolend.model.enums.EquipmentStatus;
import rw.auca.lectrolend.model.enums.EquipmentType;

public record EquipmentDto(Long id, String name, EquipmentType type, String assetTag,
                           EquipmentStatus status, String description, boolean reservable) {}
