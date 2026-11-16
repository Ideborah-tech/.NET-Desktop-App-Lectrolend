package rw.auca.lectrolend.controller;

import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.web.bind.annotation.*;
import rw.auca.lectrolend.dto.request.ChangePasswordRequest;
import rw.auca.lectrolend.dto.request.LoginRequest;
import rw.auca.lectrolend.dto.request.RegisterRequest;
import rw.auca.lectrolend.dto.request.UpdateProfileRequest;
import rw.auca.lectrolend.dto.response.AuthResponse;
import rw.auca.lectrolend.dto.response.UserDto;
import rw.auca.lectrolend.security.AuthUser;
import rw.auca.lectrolend.service.AuthService;
import rw.auca.lectrolend.service.UserService;

@RestController
@RequestMapping("/api/auth")
@RequiredArgsConstructor
public class AuthController {

    private final AuthService auth;
    private final UserService users;

    @PostMapping("/login")
    public AuthResponse login(@Valid @RequestBody LoginRequest req) {
        return auth.login(req);
    }

    @PostMapping("/register")
    @ResponseStatus(HttpStatus.CREATED)
    public AuthResponse register(@Valid @RequestBody RegisterRequest req) {
        return auth.register(req);
    }

    @GetMapping("/me")
    public UserDto me(@AuthenticationPrincipal AuthUser principal) {
        return users.get(principal.id());
    }

    @PutMapping("/me")
    public UserDto updateMe(@AuthenticationPrincipal AuthUser principal, @Valid @RequestBody UpdateProfileRequest req) {
        return users.updateProfile(principal.id(), req);
    }

    @PostMapping("/change-password")
    @ResponseStatus(HttpStatus.NO_CONTENT)
    public void changePassword(@AuthenticationPrincipal AuthUser principal, @Valid @RequestBody ChangePasswordRequest req) {
        users.changePassword(principal.id(), req);
    }
}
