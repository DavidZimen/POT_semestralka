namespace Domain.Dto;

/// <summary>
/// Dto for transferring data about episode to UI.
/// </summary>
public class EpisodeDto
{
    public Guid EpisodeId { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public DateOnly ReleaseDate { get; set; }
    
    public int Duration { get; set; }
    
    public Guid DirectorId { get; set; }
    
    public Guid SeasonId { get; set; }
}

/// <summary>
/// Request for creating new episode.
/// </summary>
/// <param name="Title">Name of the episode</param>
/// <param name="Description">Description for episode.</param>
/// <param name="ReleaseDate">Date of the premiere</param>
/// <param name="Duration">Duration of the episode.</param>
/// <param name="DirectorPersonId">Person that directed the episode.</param>
/// <param name="SeasonId">Season in which the episode aired.</param>
public record EpisodeCreate(string Title, string Description, DateOnly ReleaseDate, int Duration, Guid DirectorPersonId, Guid SeasonId);

/// <summary>
/// Request for updating the episode episode.
/// </summary>
/// <param name="Title">Name of the episode</param>
/// <param name="Description">Description for episode.</param>
/// <param name="ReleaseDate">Date of the premiere</param>
/// <param name="Duration">Duration of the episode.</param>
/// <param name="DirectorPersonId">Person that directed the episode.</param>
public record EpisodeUpdate(Guid EpisodeId, string Title, string Description, DateOnly ReleaseDate, int Duration, Guid DirectorPersonId);