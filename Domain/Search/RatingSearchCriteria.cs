namespace Domain.Search;

public record RatingSearchCriteria
{
    public Guid? FilmId { get; set; }
    public Guid? ShowId { get; set; }
    public Guid? EpisodeId { get; set; }
    public string? UserId { get; set; }
}