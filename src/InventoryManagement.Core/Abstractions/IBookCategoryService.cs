using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Core.Abstractions;

/// <summary>
/// Default implementation is AsNoTracking true.
/// </summary>
public interface IBookCategoryService : IEntityService<BookCategory>
{
    /// <summary>
    /// Get BookCategory by Name.
    /// </summary>
    /// <param name="name">A Name as string</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/></param>
    /// <returns>See <see cref="BookCategory">BookCategory</see></returns>
    Task<BookCategory?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check is name exist
    /// </summary>
    /// <param name="name">A Name as string</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/></param>
    /// <returns>bool</returns>
    Task<bool> IsBookCategoryExistAsync(string name, CancellationToken cancellationToken = default);
}