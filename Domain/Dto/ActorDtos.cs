namespace Domain.Dto;

/// <summary>
/// Dto for transferring data about Actor to UI.
/// </summary>
public class ActorDto
{
    /// <summary>
    /// Unique ID of actor.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Information about actor as person.
    /// </summary>
    public required PersonDto Person { get; set; }

    /// <summary>
    /// Number of shows, the actor played in.
    /// </summary>
    public int ShowCount { get; set; } = 0;

    /// <summary>
    /// Number of films the actor played in.
    /// </summary>
    public int FilmCount { get; set; } = 0;
}