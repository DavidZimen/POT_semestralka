namespace Domain.Entity.Abstraction;

public interface IAuditableEntity
{
    public DateTime CreatedDate { get; set; }
    
    public DateTime? ModifiedDate { get; set; }
    
    public DateTime? DeletedDate { get; set; }
    
    public string LastModifiedBy { get; set; }
}