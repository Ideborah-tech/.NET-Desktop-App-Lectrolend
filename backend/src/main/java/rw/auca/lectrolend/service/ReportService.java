package rw.auca.lectrolend.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import rw.auca.lectrolend.dto.response.ReportDto;
import rw.auca.lectrolend.model.Reservation;
import rw.auca.lectrolend.model.enums.EquipmentStatus;
import rw.auca.lectrolend.model.enums.ReservationStatus;
import rw.auca.lectrolend.repository.*;

import java.nio.charset.StandardCharsets;
import java.time.format.DateTimeFormatter;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;

@Service
@RequiredArgsConstructor
public class ReportService {

    private final EquipmentRepository equipment;
    private final ReservationRepository reservations;
    private final ReturnRecordRepository returns;
    private final UserRepository users;

    private static final DateTimeFormatter FMT = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm");

    @Transactional(readOnly = true)
    public ReportDto summary() {
        Map<String, Long> eq = new LinkedHashMap<>();
        for (EquipmentStatus s : EquipmentStatus.values()) eq.put(s.name(), equipment.countByStatus(s));
        Map<String, Long> res = new LinkedHashMap<>();
        for (ReservationStatus s : ReservationStatus.values()) res.put(s.name(), reservations.countByStatus(s));
        List<ReportDto.Popular> popular = reservations.mostBorrowed().stream().limit(5)
                .map(row -> new ReportDto.Popular((String) row[0], (Long) row[1])).toList();
        return new ReportDto(equipment.count(), eq, res, returns.countByLateReturnTrue(), users.count(), popular);
    }

    @Transactional(readOnly = true)
    public byte[] generateCsv() {
        List<Reservation> all = reservations.findAllByOrderByStartTimeDesc();
        StringBuilder sb = new StringBuilder();
        sb.append("ID,Borrower,Email,Equipment,Asset Tag,Status,Start,End,Room,Late Return\n");
        for (Reservation r : all) {
            sb.append(r.getReservationId()).append(',');
            sb.append(csv(r.getUser().getFullName())).append(',');
            sb.append(csv(r.getUser().getEmail())).append(',');
            sb.append(csv(r.getEquipment().getName())).append(',');
            sb.append(csv(r.getEquipment().getAssetTag())).append(',');
            sb.append(r.getStatus()).append(',');
            sb.append(r.getStartTime().format(FMT)).append(',');
            sb.append(r.getEndTime().format(FMT)).append(',');
            sb.append(r.getRoom() != null ? csv(r.getRoom().getRoomCode()) : "").append(',');
            boolean late = r.getReturnRecord() != null && r.getReturnRecord().isLateReturn();
            sb.append(late ? "Yes" : "No").append('\n');
        }
        return sb.toString().getBytes(StandardCharsets.UTF_8);
    }

    private static String csv(String v) {
        if (v == null) return "";
        if (v.contains(",") || v.contains("\"") || v.contains("\n"))
            return "\"" + v.replace("\"", "\"\"") + "\"";
        return v;
    }
}
