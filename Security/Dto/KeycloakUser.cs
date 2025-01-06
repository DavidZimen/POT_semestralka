using Security.Enums;

namespace Security.Dto;

public record KeycloakUser(string Email, string Password, string FirstName, string LastName, string Role);