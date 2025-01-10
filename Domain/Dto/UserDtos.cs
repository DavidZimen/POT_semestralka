namespace Domain.Dto;

/// <summary>
/// Record for register a new user to the application.
/// </summary>
public record RegisterUser(string Email, string Password, string FirstName, string LastName, string Role);