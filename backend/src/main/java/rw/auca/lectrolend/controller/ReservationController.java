package rw.auca.lectrolend.controller;

import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.web.bind.annotation.*;
import rw.auca.lectrolend.dto.request.BatchReservationRequest;
import rw.auca.lectrolend.dto.request.ReservationRequest;
import rw.auca.lectrolend.dto.request.ReturnRequest;
import rw.auca.lectrolend.dto.response.ReservationDto;
import rw.auca.lectrolend.model.enums.ReservationStatus;
import rw.auca.lectrolend.model.enums.Role;
import rw.auca.lectrolend.security.AuthUser;
import rw.auca.lectrolend.service.ReservationService;

import java.util.List;

@RestController
@RequestMapping("/api/reservations")
@RequiredArgsConstructor
public class ReservationController {

    private final ReservationService service;

    @PostMapping
    @PreAuthorize("hasAnyRole('STUDENT','LECTURER')")
    @ResponseStatus(HttpStatus.CREATED)
    public ReservationDto create(@AuthenticationPrincipal AuthUser user, @Valid @RequestBody ReservationRequest req) {
        return service.create(user.id(), req);
    }

    @PostMapping("/batch")
    @PreAuthorize("hasAnyRole('STUDENT','LECTURER')")
    @ResponseStatus(HttpStatus.CREATED)
    public List<ReservationDto> createBatch(@AuthenticationPrincipal AuthUser user, @Valid @RequestBody BatchReservationRequest req) {
        return service.createBatch(user.id(), req);
    }

    @GetMapping("/mine")
    public List<ReservationDto> mine(@AuthenticationPrincipal AuthUser user) {
        return service.mine(user.id());
    }

    @GetMapping
    @PreAuthorize("hasRole('ADMIN')")
    public List<ReservationDto> all(@RequestParam(required = false) ReservationStatus status) {
        return service.all(status);
    }

    @PostMapping("/{id}/cancel")
    public ReservationDto cancel(@AuthenticationPrincipal AuthUser user, @PathVariable Long id) {
        return service.cancel(id, user.id(), user.role() == Role.ADMIN);
    }

    @PostMapping("/{id}/issue")
    @PreAuthorize("hasRole('ADMIN')")
    public ReservationDto issue(@PathVariable Long id) {
        return service.issue(id);
    }

    @PostMapping("/{id}/return")
    @PreAuthorize("hasRole('ADMIN')")
    public ReservationDto confirmReturn(@AuthenticationPrincipal AuthUser admin, @PathVariable Long id,
                                        @RequestBody(required = false) ReturnRequest req) {
        return service.confirmReturn(id, admin.id(), req);
    }
}
