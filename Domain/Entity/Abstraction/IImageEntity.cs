namespace Domain.Entity.Abstraction;

public interface IImageEntity
{
    public Guid? ImageId { get; set; }
    
    public ImageEntity? Image { get; set; }
}