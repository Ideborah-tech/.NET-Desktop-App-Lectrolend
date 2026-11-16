package rw.auca.lectrolend.dto.request;

import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;
import rw.auca.lectrolend.model.enums.Role;

public record RegisterRequest(
        @NotBlank(message = "Full name is required") @Size(max = 100) String fullName,
        @NotBlank(message = "Email is required") @Email(message = "Enter a valid email") @Size(max = 100) String email,
        @NotBlank(message = "Password is required") @Size(min = 8, max = 72, message = "Password must be 8 to 72 characters") String password,
        @Size(max = 20) String phone,
        @NotNull(message = "Role is required") Role role,
        @Size(max = 20) String registrationNo,
        @Size(max = 80) String faculty,
        @Size(max = 80) String program,
        @Size(max = 20) String staffNo,
        @Size(max = 80) String department) {}
