using InventoryManagement.Shared.Abstractions.Entities;

namespace InventoryManagement.Domain.Entities;

public sealed class Publisher : BaseEntity, IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Address { get; set; }
}