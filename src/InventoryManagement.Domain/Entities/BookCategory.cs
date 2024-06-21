using InventoryManagement.Shared.Abstractions.Entities;

namespace InventoryManagement.Domain.Entities;

public sealed class BookCategory : BaseEntity, IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
}