package rw.auca.lectrolend.model;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;
import rw.auca.lectrolend.model.enums.Role;

@Entity
@Table(name = "student")
@DiscriminatorValue("STUDENT")
@PrimaryKeyJoinColumn(name = "user_id")
@Getter
@Setter
public class Student extends Borrower {

    @Column(name = "registration_no", nullable = false, unique = true, length = 20)
    private String registrationNo;

    @Column(length = 80)
    private String faculty;

    @Column(length = 80)
    private String program;

    @Override
    public Role getRole() { return Role.STUDENT; }

    @Override
    public int getMaxActiveReservations() { return 3; }
}
