using InventoryManagement.Shared.Abstractions.Entities;

namespace InventoryManagement.Domain.Entities;

public sealed class Author : BaseEntity, IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public string? Biography { get; set; }
    public string? Image { get; set; }
}