package rw.auca.lectrolend.model;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;
import rw.auca.lectrolend.model.enums.Role;

@Entity
@Table(name = "lecturer")
@DiscriminatorValue("LECTURER")
@PrimaryKeyJoinColumn(name = "user_id")
@Getter
@Setter
public class Lecturer extends Borrower {

    @Column(name = "staff_no", nullable = false, unique = true, length = 20)
    private String staffNo;

    @Column(length = 80)
    private String department;

    @Override
    public Role getRole() { return Role.LECTURER; }

    @Override
    public int getMaxActiveReservations() { return 5; }
}
