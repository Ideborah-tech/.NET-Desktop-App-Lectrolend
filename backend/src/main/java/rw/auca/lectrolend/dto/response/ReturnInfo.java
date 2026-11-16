package rw.auca.lectrolend.dto.response;

import java.time.LocalDateTime;

public record ReturnInfo(LocalDateTime returnedAt, String conditionOnReturn, boolean lateReturn,
                         String remarks, String confirmedBy) {}
