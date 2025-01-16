namespace Domain.Dto;

/// <summary>
/// Dto for creating new genre in DB.
/// </summary>
/// <param name="Name">Name of the genre to be created.</param>
public record GenreCreate(string Name);

/// <summary>
/// Dto for requesting update of the genre.
/// </summary>
/// <param name="GenreId"></param>
/// <param name="Name"></param>
public record GenreUpdate(Guid GenreId, string Name);

/// <summary>
/// Dto for sending data to the application UI.
/// </summary>
/// <param name="GenreId">Unique ID of the genre.</param>
/// <param name="Name">Name of the genre.</param>
public class GenreDto 
{
    public Guid GenreId { get; set; }
    public string Name { get; set; } = string.Empty;
};