package rw.auca.lectrolend.dto.response;

import rw.auca.lectrolend.model.enums.Role;

public record UserDto(Long id, String fullName, String email, String phone, Role role, boolean active,
                      String registrationNo, String faculty, String program,
                      String staffNo, String department) {}
