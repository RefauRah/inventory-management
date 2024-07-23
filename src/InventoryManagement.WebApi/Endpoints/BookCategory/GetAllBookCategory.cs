using InventoryManagement.Shared.Abstractions.Databases;
using InventoryManagement.WebApi.Contracts.Responses;
using InventoryManagement.WebApi.Endpoints.BookCategory.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryManagement.WebApi.Endpoints.BookCategory;

public class GetAllBookCategory : BaseEndpoint<GetAllBookCategoryRequest, List<BookCategoryResponse>>
{
    private readonly IDbContext _dbContext;

    public GetAllBookCategory(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    //[Authorize]
    //[RequiredScope(typeof(BookCategoryScope), typeof(BookCategoryScopeReadOnly))]
    [SwaggerOperation(
        Summary = "Get Book Categories",
        Description = "",
        OperationId = "BookCategory.GetAllBookCategory",
        Tags = new[] { "BookCategory" })
    ]
    [ProducesResponseType(typeof(List<BookCategoryResponse>), StatusCodes.Status200OK)]
    public override async Task<ActionResult<List<BookCategoryResponse>>> HandleAsync([FromQuery] GetAllBookCategoryRequest request,
        CancellationToken cancellationToken = new())
    {
        var queryable = _dbContext.Set<Domain.Entities.BookCategory>().AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search) && request.Search.Length > 2)
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{request.Search}%"));

        var data = await queryable
            .Select(e => new BookCategoryResponse
            {
                Id = e.Id,
                Name = e.Name
            })
            .ToListAsync(cancellationToken);

        return data;
    }
}