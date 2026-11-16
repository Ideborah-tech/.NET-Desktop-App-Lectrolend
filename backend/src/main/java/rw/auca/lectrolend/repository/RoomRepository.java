package rw.auca.lectrolend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import rw.auca.lectrolend.model.Room;

public interface RoomRepository extends JpaRepository<Room, Long> {
    boolean existsByRoomCodeIgnoreCase(String roomCode);
}
