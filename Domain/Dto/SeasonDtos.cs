namespace Domain.Dto;

public class SeasonDto
{
    public Guid SeasonId { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public Guid ShowId { get; set; }
}

/// <summary>
/// Dto for requesting creation of the season.
/// </summary>
/// <param name="Title">Name of the season</param>
/// <param name="Description">Description of the season</param>
/// <param name="ShowId">ShowId, to which the season belongs.</param>
public record SeasonCreate(string Title, string Description, Guid ShowId);

/// <summary>
/// Dto for requesting update of the season.
/// ShowId not provided, season cannot be transferred to different show.
/// </summary>
/// <param name="SeasonId">ID of the season.</param>
/// <param name="Title">New title of the season.</param>
/// <param name="Description">New Description of the season.</param>
public record SeasonUpdate(Guid SeasonId, string Title, string Description);