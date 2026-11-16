package rw.auca.lectrolend.model;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;

@Entity
@Table(name = "room")
@Getter
@Setter
public class Room {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "room_id")
    private Long roomId;

    @Column(name = "room_code", nullable = false, unique = true, length = 20)
    private String roomCode;

    @Column(nullable = false, length = 60)
    private String building;

    @Column(nullable = false)
    private int capacity;

    public String getDetails() {
        return roomCode + " (" + building + ", capacity " + capacity + ")";
    }
}
