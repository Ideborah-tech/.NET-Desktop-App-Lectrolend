package rw.auca.lectrolend.service;

import lombok.RequiredArgsConstructor;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.authentication.DisabledException;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import rw.auca.lectrolend.dto.request.LoginRequest;
import rw.auca.lectrolend.dto.request.RegisterRequest;
import rw.auca.lectrolend.dto.response.AuthResponse;
import rw.auca.lectrolend.model.User;
import rw.auca.lectrolend.repository.UserRepository;
import rw.auca.lectrolend.security.JwtService;

@Service
@RequiredArgsConstructor
public class AuthService {

    private final UserRepository users;
    private final PasswordEncoder encoder;
    private final JwtService jwt;
    private final UserService userService;

    @Transactional(readOnly = true)
    public AuthResponse login(LoginRequest req) {
        User u = users.findByEmailIgnoreCase(req.email().trim())
                .orElseThrow(() -> new BadCredentialsException("Incorrect email or password"));
        if (!encoder.matches(req.password(), u.getPasswordHash())) {
            throw new BadCredentialsException("Incorrect email or password");
        }
        if (!u.isActive()) {
            throw new DisabledException("This account has been deactivated. Please contact the IT office.");
        }
        return new AuthResponse(jwt.generate(u), UserService.toDto(u));
    }

    /** Public self-registration is limited to borrowers (students and lecturers). */
    @Transactional
    public AuthResponse register(RegisterRequest req) {
        User u = userService.create(req, false);
        return new AuthResponse(jwt.generate(u), UserService.toDto(u));
    }
}
