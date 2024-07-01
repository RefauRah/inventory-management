using InventoryManagement.Core.Abstractions;
using InventoryManagement.WebApi.Contracts.Requests;
using InventoryManagement.WebApi.Endpoints.BookCategory.Requests;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryManagement.WebApi.Endpoints.BookCategory;

public class DeleteBookCategory : BaseEndpointWithoutResponse<DeleteBookCategoryRequest>
{
    private readonly IBookCategoryService _bookCategory;

    public DeleteBookCategory(IBookCategoryService BookCategory)
    {
        _bookCategory = BookCategory;
    }

    [HttpDelete("{Id}")]
    //[Authorize]
    //[RequiredScope(typeof(BookCategoryScope))]
    [SwaggerOperation(
        Summary = "Remove data from Book Category by Id",
        Description = "",
        OperationId = "BookCategory.DeleteBookCategory",
        Tags = new[] { "BookCategory" })
    ]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public override async Task<ActionResult> HandleAsync([FromRoute] DeleteBookCategoryRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        await _bookCategory.DeleteAsync(request.Id);
        return Ok();
    }
}