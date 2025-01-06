namespace Domain.Entity.Abstraction;

/// <summary>
/// This interface represents, that user can own the implementing entity and will be used
/// in checking the ownership when doing certain operations.
/// </summary>
public interface IOwnerableEntity
{
    /// <summary>
    /// Unique ID of the user.
    /// </summary>
    public string UserId { get; }

    public bool IsOwner(string? userId)
    {
        return UserId.Equals(userId);
    }
}