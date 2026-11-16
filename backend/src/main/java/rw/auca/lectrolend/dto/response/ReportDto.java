package rw.auca.lectrolend.dto.response;

import java.util.List;
import java.util.Map;

public record ReportDto(
        long totalEquipment,
        Map<String, Long> equipmentByStatus,
        Map<String, Long> reservationsByStatus,
        long lateReturns,
        long totalUsers,
        List<Popular> mostBorrowed) {

    public record Popular(String name, long count) {}
}
