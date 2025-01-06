namespace Domain.Dto;

/// <summary>
/// Dto object for transferring rating entity to the UI.
/// </summary>
/// <param name="Id">Unique ID of the rating.</param>
/// <param name="UserId">ID of user, that submitted the rating.</param>
/// <param name="Value">Numeric 1-10 value.</param>
/// <param name="Description">Brief text description.</param>
public record RatingDto(Guid Id, string UserId, int Value, string? Description);

/// <summary>
/// Dto for transferring average rating of the film, show or episode to the UI.
/// </summary>
/// <param name="Average">Average from all ratings.</param>
/// <param name="Count">Number of ratings submitted.</param>
public record AverageRatingDto(double? Average, int Count);

/// <summary>
/// Dto for creation of the new rating entry in DB.
/// Only one of FilmId, ShowId and EpisodeId must not be null.
/// </summary>
/// <param name="Value">Value of the rating. From 1 to 10.</param>
/// <param name="Description">Not required brief text of rating.</param>
/// <param name="FilmId">Film to be rated.</param>
/// <param name="ShowId">Show to be rated.</param>
/// <param name="EpisodeId">Episode to be rated.</param>
public record CreateRatingRequest(int Value, string? Description, Guid? FilmId = null, Guid? ShowId = null, Guid? EpisodeId = null);

/// <summary>
/// Dto for updating existing rating entry.
/// </summary>
/// <param name="Id">Unique ID of rating to be updated.</param>
/// <param name="UserId">User that submitted the rating.</param>
/// <param name="Value">New numeric value 1-10.</param>
/// <param name="Description">New brief text description.</param>
public record UpdateRatingRequest(Guid Id, string UserId, int Value, string? Description);

/// <summary>
/// Dto for deleting rating from DB.
/// </summary>
/// <param name="UserId"></param>
public record DeleteRatingRequest(Guid Id, string UserId);

/// <summary>
/// Dto for requesting users rating to the film, show or episode.
/// Only one of FilmId, ShowId and EpisodeId must not be null.
/// </summary>
/// <param name="UserId">ID of user to be requested.</param>
/// <param name="FilmId">Film that should have the users rating.</param>
/// <param name="ShowId">Show that should have the users rating.</param>
/// <param name="EpisodeId">Episode that should have the users rating.</param>
public record UserRatingRequest(string UserId, Guid? FilmId = null, Guid? ShowId = null, Guid? EpisodeId = null);

/// <summary>
/// Dto to request average rating of film, show or episode.
/// Only one of FilmId, ShowId and EpisodeId must not be null.
/// </summary>
/// <param name="FilmId">Film for average rating.</param>
/// <param name="ShowId">Show for average rating.</param>
/// <param name="EpisodeId">Episode for average rating.</param>
public record AverageRatingRequest(Guid? FilmId = null, Guid? ShowId = null, Guid? EpisodeId = null);