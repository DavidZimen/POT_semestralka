namespace Domain.Dto;

public class ImageIdDto
{
    public Guid? ImageId { get; set; }
}

public record ImageCreate(byte[] Content, string Name, string Type, Guid? PersonId, Guid? FilmId, Guid? ShowId, Guid? EpisodeId);