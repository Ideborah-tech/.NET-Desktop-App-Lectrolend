package rw.auca.lectrolend.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import rw.auca.lectrolend.dto.request.RoomRequest;
import rw.auca.lectrolend.dto.response.RoomDto;
import rw.auca.lectrolend.exception.BadRequestException;
import rw.auca.lectrolend.exception.NotFoundException;
import rw.auca.lectrolend.model.Room;
import rw.auca.lectrolend.repository.RoomRepository;

import java.util.Comparator;
import java.util.List;

@Service
@RequiredArgsConstructor
public class RoomService {

    private final RoomRepository rooms;

    static RoomDto toDto(Room r) {
        return new RoomDto(r.getRoomId(), r.getRoomCode(), r.getBuilding(), r.getCapacity(), r.getDetails());
    }

    @Transactional(readOnly = true)
    public List<RoomDto> list() {
        return rooms.findAll().stream().sorted(Comparator.comparing(Room::getRoomCode)).map(RoomService::toDto).toList();
    }

    @Transactional
    public RoomDto create(RoomRequest req) {
        String code = req.roomCode().trim();
        if (rooms.existsByRoomCodeIgnoreCase(code)) throw new BadRequestException("Room code already exists");
        Room r = new Room();
        apply(r, req);
        return toDto(rooms.save(r));
    }

    @Transactional
    public RoomDto update(Long id, RoomRequest req) {
        Room r = find(id);
        if (!r.getRoomCode().equalsIgnoreCase(req.roomCode().trim()) && rooms.existsByRoomCodeIgnoreCase(req.roomCode().trim())) {
            throw new BadRequestException("Room code already exists");
        }
        apply(r, req);
        return toDto(r);
    }

    @Transactional
    public void delete(Long id) {
        rooms.delete(find(id));
        rooms.flush(); // surfaces FK violation (room used by reservations) as a clean 409
    }

    private Room find(Long id) {
        return rooms.findById(id).orElseThrow(() -> new NotFoundException("Room not found"));
    }

    private void apply(Room r, RoomRequest req) {
        r.setRoomCode(req.roomCode().trim());
        r.setBuilding(req.building().trim());
        r.setCapacity(req.capacity());
    }
}
