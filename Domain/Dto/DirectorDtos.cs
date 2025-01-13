namespace Domain.Dto;

/// <summary>
/// Object for transferring data with Film related to Director.
/// </summary>
public class DirectorFilmDto
{
    public Guid FilmId { get; set; }
    
    public string Title { get; set; }
}

/// <summary>
/// Object for transferring data with Show related to Director.
/// </summary>
public class DirectorShowDto
{
    public Guid ShowId { get; set; }
    
    public string Title { get; set; }
    
    /// <summary>
    /// Total number of episodes, the director directed for the show.
    /// </summary>
    public int EpisodeCount { get; set; }
}