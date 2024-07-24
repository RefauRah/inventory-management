using InventoryManagement.Core.Abstractions;
using InventoryManagement.WebApi.Contracts.Requests;
using InventoryManagement.WebApi.Endpoints.Book.Requests;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryManagement.WebApi.Endpoints.Book;

public class DeleteBook : BaseEndpointWithoutResponse<DeleteBookRequest>
{
    private readonly IBookService _book;

    public DeleteBook(IBookService Book)
    {
        _book = Book;
    }

    [HttpDelete("{Id}")]
    //[Authorize]
    //[RequiredScope(typeof(BookScope))]
    [SwaggerOperation(
        Summary = "Remove data from Book  by Id",
        Description = "",
        OperationId = "Book.DeleteBook",
        Tags = new[] { "Book" })
    ]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public override async Task<ActionResult> HandleAsync([FromRoute] DeleteBookRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        await _book.DeleteAsync(request.Id);
        return Ok();
    }
}