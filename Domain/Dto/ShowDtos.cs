namespace Domain.Dto;

public class ShowDto
{
    public Guid ShowId { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public DateOnly ReleaseDate { get; set; }
    
    public DateOnly? EndDate { get; set; }

    public int EpisodeCount { get; set; }
    
    public int SeasonCount { get; set; }

    public List<GenreDto> Genres { get; set; } = [];
}

/// <summary>
/// Dto for requesting new show creation.
/// </summary>
/// <param name="Title">Show name</param>
/// <param name="Description">Show description</param>
/// <param name="ReleaseDate">Date of first episode premiere</param>
/// <param name="EndDate">End of the show run.</param>
public record ShowCreate(string Title, string Description, DateOnly ReleaseDate, DateOnly? EndDate);

/// <summary>
/// Dto for requesting a show update.
/// </summary>
/// <param name="ShowId">ID of the show to be updated.</param>
/// <param name="Title">Show name</param>
/// <param name="Description">Show description</param>
/// <param name="ReleaseDate">Date of first episode premiere</param>
/// <param name="EndDate">End of the show run.</param>
public record ShowUpdate(Guid ShowId, string Title, string Description, DateOnly ReleaseDate, DateOnly? EndDate);