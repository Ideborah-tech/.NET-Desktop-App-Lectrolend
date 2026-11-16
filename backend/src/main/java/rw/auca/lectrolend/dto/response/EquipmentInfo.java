package rw.auca.lectrolend.dto.response;

import rw.auca.lectrolend.model.enums.EquipmentStatus;
import rw.auca.lectrolend.model.enums.EquipmentType;

public record EquipmentInfo(Long id, String name, String assetTag, EquipmentType type, EquipmentStatus status) {}
