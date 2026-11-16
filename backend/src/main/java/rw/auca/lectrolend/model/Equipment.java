package rw.auca.lectrolend.model;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;
import rw.auca.lectrolend.model.enums.EquipmentStatus;
import rw.auca.lectrolend.model.enums.EquipmentType;

@Entity
@Table(name = "equipment")
@Getter
@Setter
public class Equipment {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "equipment_id")
    private Long equipmentId;

    @ManyToOne(fetch = FetchType.LAZY, optional = false)
    @JoinColumn(name = "added_by", nullable = false)
    private Admin addedBy;

    @Column(nullable = false, length = 80)
    private String name;

    @Enumerated(EnumType.STRING)
    @Column(nullable = false, length = 20)
    private EquipmentType type;

    @Column(name = "asset_tag", nullable = false, unique = true, length = 30)
    private String assetTag;

    @Enumerated(EnumType.STRING)
    @Column(nullable = false, length = 20)
    private EquipmentStatus status = EquipmentStatus.AVAILABLE;

    @Column(columnDefinition = "TEXT")
    private String description;

    public void updateStatus(EquipmentStatus newStatus) {
        this.status = newStatus;
    }

    /** On the shelf and ready to hand over right now. */
    public boolean isAvailable() {
        return status == EquipmentStatus.AVAILABLE;
    }

    /**
     * Can accept new bookings. An item that is currently IN_USE can still be reserved for a later
     * slot; only maintenance and retirement take it out of circulation.
     */
    public boolean isReservable() {
        return status == EquipmentStatus.AVAILABLE || status == EquipmentStatus.IN_USE;
    }
}
