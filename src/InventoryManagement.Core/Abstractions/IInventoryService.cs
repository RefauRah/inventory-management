using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Core.Abstractions;

/// <summary>
/// Default implementation is AsNoTracking true.
/// </summary>
public interface IInventoryService : IEntityService<Inventory>
{
    /// <summary>
    /// Get BookCategory by Name.
    /// </summary>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/></param>
    Task<Inventory?> AddAsync(Inventory entity, CancellationToken cancellationToken = default);
}