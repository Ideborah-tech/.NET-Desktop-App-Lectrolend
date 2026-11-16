package rw.auca.lectrolend.security;

import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;
import rw.auca.lectrolend.model.enums.Role;
import rw.auca.lectrolend.model.User;

import java.util.Collection;
import java.util.List;

/** Authenticated principal placed in the SecurityContext for each request. */
public record AuthUser(Long id, String email, Role role, boolean active) implements UserDetails {

    public static AuthUser of(User u) {
        return new AuthUser(u.getUserId(), u.getEmail(), u.getRole(), u.isActive());
    }

    @Override public Collection<? extends GrantedAuthority> getAuthorities() {
        return List.of(new SimpleGrantedAuthority("ROLE_" + role.name()));
    }
    @Override public String getPassword() { return null; }
    @Override public String getUsername() { return email; }
    @Override public boolean isEnabled() { return active; }
}
