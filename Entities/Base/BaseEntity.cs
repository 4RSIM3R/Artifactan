namespace Artifactan.Entities.Base;

public abstract class BaseEntity
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}