package rw.auca.lectrolend.controller;

import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.web.bind.annotation.*;
import rw.auca.lectrolend.dto.request.ActiveRequest;
import rw.auca.lectrolend.dto.request.RegisterRequest;
import rw.auca.lectrolend.dto.response.UserDto;
import rw.auca.lectrolend.security.AuthUser;
import rw.auca.lectrolend.service.UserService;

import java.util.List;

@RestController
@RequestMapping("/api/users")
@PreAuthorize("hasRole('ADMIN')")
@RequiredArgsConstructor
public class UserController {

    private final UserService service;

    @GetMapping
    public List<UserDto> list() {
        return service.list();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    public UserDto create(@Valid @RequestBody RegisterRequest req) {
        return UserService.toDto(service.create(req, true));
    }

    @PatchMapping("/{id}/active")
    public UserDto setActive(@AuthenticationPrincipal AuthUser admin, @PathVariable Long id,
                             @RequestBody ActiveRequest req) {
        return service.setActive(id, req.active(), admin.id());
    }
}
