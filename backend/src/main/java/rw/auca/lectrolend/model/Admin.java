package rw.auca.lectrolend.model;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;
import rw.auca.lectrolend.model.enums.Role;

@Entity
@Table(name = "admin")
@DiscriminatorValue("ADMIN")
@PrimaryKeyJoinColumn(name = "user_id")
@Getter
@Setter
public class Admin extends User {

    @Column(name = "staff_no", nullable = false, unique = true, length = 20)
    private String staffNo;

    @Override
    public Role getRole() { return Role.ADMIN; }
}
