package rw.auca.lectrolend.dto.response;

import java.util.List;

public record AvailabilityDto(boolean available, String reason, List<Slot> busySlots) {}
