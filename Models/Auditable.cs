namespace kuro_desserts.Models;

public abstract class Auditable
{
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;
}