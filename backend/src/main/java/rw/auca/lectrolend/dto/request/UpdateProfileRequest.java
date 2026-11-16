package rw.auca.lectrolend.dto.request;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;

public record UpdateProfileRequest(
        @NotBlank(message = "Full name is required") @Size(max = 100, message = "Name is too long") String fullName,
        @Size(max = 20, message = "Phone number is too long") String phone,
        @Size(max = 80) String faculty,
        @Size(max = 80) String program,
        @Size(max = 80) String department) {}
