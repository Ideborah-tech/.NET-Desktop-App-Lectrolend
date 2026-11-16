package rw.auca.lectrolend.model;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;

import java.time.LocalDateTime;

@Entity
@Table(name = "return_record")
@Getter
@Setter
public class ReturnRecord {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "return_id")
    private Long returnId;

    @OneToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "reservation_id", nullable = false, unique = true)
    private Reservation reservation;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "confirmed_by", nullable = false)
    private Admin confirmedBy;

    @Column(name = "returned_at", nullable = false)
    private LocalDateTime returnedAt;

    @Column(name = "condition_on_return", nullable = false, length = 100)
    private String conditionOnReturn;

    @Column(name = "late_return", nullable = false)
    private boolean lateReturn;

    @Column(columnDefinition = "TEXT")
    private String remarks;
}
