namespace Domain.Dto;

public class CharacterDto
{
    public Guid CharacterId { get; set; }
    
    public Guid? FilmId { get; set; }
    
    public Guid? ShowId { get; set; }
    
    public Guid ActorId { get; set; }
    
    public required string CharacterName { get; set; }
}

/// <summary>
/// Dto for sending data about media with relate character of the actor.
/// </summary>
public class CharacterActorDto
{
    public Guid? ShowId { get; set; }
    
    public Guid? FilmId { get; set; }
    
    /// <summary>
    /// Name of the show, or film.
    /// </summary>
    public string MediaName { get; set; } = string.Empty;
    
    /// <summary>
    /// Name of the character actor played.
    /// </summary>
    public string CharacterName { get; set; } = string.Empty;
    
    /// <summary>
    /// Date when the film or show premiered.
    /// </summary>
    public DateOnly PremiereDate { get; set; }
    
    /// <summary>
    /// Date when, the show ended.
    /// With film always null.
    /// </summary>
    public DateOnly? EndDate { get; set; }
}

/// <summary>
/// Dto for sending character information in film.
/// </summary>
public class CharacterMediaDto
{
    public Guid CharacterId { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string CharacterName { get; set; } = string.Empty;
    
    public required PersonMinimalDto Actor { get; set; }
}

/// <summary>
/// Dto for requesting to add Person to Film or Show as Actor with character name in the media type.
/// </summary>
/// <param name="FilmId">Film, where the character played.</param>
/// <param name="CharacterName">Name of the character in the movie.</param>
/// <param name="ActorPersonId">Person ID of the actor.</param>
public record CharacterCreate(string CharacterName, Guid? FilmId, Guid? ShowId, Guid ActorPersonId);