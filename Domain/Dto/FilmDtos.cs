namespace Domain.Dto;

public class FilmDto
{
    public string FilmId { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateOnly ReleaseDate { get; set; }
    
    public int Duration { get; set; }
    
    public Guid DirectorId { get; set; }
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