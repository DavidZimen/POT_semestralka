namespace Domain.Dto;

public class FilmDto : ImageIdDto
{
    public Guid FilmId { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public DateOnly ReleaseDate { get; set; }
    
    public int Duration { get; set; }
    
    public Guid DirectorPersonId { get; set; }

    public List<GenreDto> Genres { get; set; } = [];
}

/// <summary>
/// Dto for requesting for new film creation.
/// </summary>
/// <param name="Title">Film name</param>
/// <param name="Description">Brief description of the film.</param>
/// <param name="ReleaseDate">Date of premiere.</param>
/// <param name="Duration">Duration in minutes.</param>
/// <param name="DirectorPersonId">PersonId of the director.</param>
public record FilmCreate(string Title, string Description, DateOnly ReleaseDate, int Duration, Guid DirectorPersonId);

/// <summary>
/// Dto for requesting updating of the film.
/// </summary>
/// <param name="Title">Film name</param>
/// <param name="Description">Brief description of the film.</param>
/// <param name="ReleaseDate">Date of premiere.</param>
/// <param name="Duration">Duration in minutes.</param>
/// <param name="DirectorPersonId">PersonId of the director.</param>
public record FilmUpdate(Guid FilmId, string Title, string Description, DateOnly ReleaseDate, int Duration, Guid DirectorPersonId);
