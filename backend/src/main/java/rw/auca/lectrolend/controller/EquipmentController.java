package rw.auca.lectrolend.controller;

import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.web.bind.annotation.*;
import rw.auca.lectrolend.dto.request.EquipmentRequest;
import rw.auca.lectrolend.dto.request.StatusRequest;
import rw.auca.lectrolend.dto.response.AvailabilityDto;
import rw.auca.lectrolend.dto.response.EquipmentDto;
import rw.auca.lectrolend.model.enums.EquipmentStatus;
import rw.auca.lectrolend.model.enums.EquipmentType;
import rw.auca.lectrolend.security.AuthUser;
import rw.auca.lectrolend.service.EquipmentService;

import java.time.LocalDateTime;
import java.util.List;

@RestController
@RequestMapping("/api/equipment")
@RequiredArgsConstructor
public class EquipmentController {

    private final EquipmentService service;

    @GetMapping
    public List<EquipmentDto> list(@RequestParam(required = false) EquipmentType type,
                                   @RequestParam(required = false) EquipmentStatus status,
                                   @RequestParam(required = false) String q) {
        return service.list(type, status, q);
    }

    @GetMapping("/{id}")
    public EquipmentDto get(@PathVariable Long id) {
        return service.get(id);
    }

    @GetMapping("/{id}/availability")
    public AvailabilityDto availability(@PathVariable Long id,
                                        @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE_TIME) LocalDateTime start,
                                        @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE_TIME) LocalDateTime end) {
        return service.availability(id, start, end);
    }

    @PostMapping
    @PreAuthorize("hasRole('ADMIN')")
    @ResponseStatus(HttpStatus.CREATED)
    public EquipmentDto create(@AuthenticationPrincipal AuthUser admin, @Valid @RequestBody EquipmentRequest req) {
        return service.create(admin.id(), req);
    }

    @PutMapping("/{id}")
    @PreAuthorize("hasRole('ADMIN')")
    public EquipmentDto update(@PathVariable Long id, @Valid @RequestBody EquipmentRequest req) {
        return service.update(id, req);
    }

    @PatchMapping("/{id}/status")
    @PreAuthorize("hasRole('ADMIN')")
    public EquipmentDto updateStatus(@PathVariable Long id, @Valid @RequestBody StatusRequest req) {
        return service.updateStatus(id, req.status());
    }

    /** Deletes unused equipment (204) or retires it when it has history (200 with the retired item). */
    @DeleteMapping("/{id}")
    @PreAuthorize("hasRole('ADMIN')")
    public ResponseEntity<EquipmentDto> remove(@PathVariable Long id) {
        EquipmentDto retired = service.remove(id);
        return retired == null ? ResponseEntity.noContent().build() : ResponseEntity.ok(retired);
    }
}
