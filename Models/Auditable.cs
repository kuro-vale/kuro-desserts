using System.Text.Json.Serialization;

namespace kuro_desserts.Models;

public abstract class Auditable
{
    [JsonIgnore] public DateTime CreatedAt { get; set; }

    [JsonIgnore] public DateTime UpdatedAt { get; set; }

    [JsonIgnore] public bool IsDeleted { get; set; } = false;
}