using InventoryManagement.Core.Abstractions;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Shared.Abstractions.Databases;
using InventoryManagement.Shared.Abstractions.Encryption;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InventoryManagement.Infrastructure.Services;

public class BookCategoryService : IBookCategoryService
{
    private readonly IDbContext _dbContext;
    private readonly ISalter _salter;

    public BookCategoryService(IDbContext dbContext, ISalter salter)
    {
        _dbContext = dbContext;
        _salter = salter;
    }

    public IQueryable<BookCategory> GetBaseQuery()
        => _dbContext.Set<BookCategory>()
            .AsQueryable();

    public Task<BookCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => GetBaseQuery()
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<BookCategory?> CreateAsync(BookCategory entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.InsertAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var BookCategory = await GetByIdAsync(id, cancellationToken);
        if (BookCategory is null)
            throw new Exception("Data not found");

        _dbContext.AttachEntity(BookCategory);

        BookCategory.SetToDeleted();

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<BookCategory?> GetByExpressionAsync(
        Expression<Func<BookCategory, bool>> predicate,
        Expression<Func<BookCategory, BookCategory>> projection,
        CancellationToken cancellationToken = default)
        => GetBaseQuery()
            .Where(predicate)
            .Select(projection)
            .FirstOrDefaultAsync(cancellationToken);

    public Task<BookCategory?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var s = name.ToUpper();

        return GetBaseQuery()
            .Where(e => e.Name.ToUpper() == s)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<bool> IsBookCategoryExistAsync(string name, CancellationToken cancellationToken = default)
    {
        name = name.ToUpper();

        return GetBaseQuery().Where(e => e.Name.ToUpper() == name)
            .AnyAsync(cancellationToken);
    }
}