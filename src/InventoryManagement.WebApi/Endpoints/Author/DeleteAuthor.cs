using InventoryManagement.Core.Abstractions;
using InventoryManagement.WebApi.Contracts.Requests;
using InventoryManagement.WebApi.Endpoints.Author.Requests;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryManagement.WebApi.Endpoints.Author;

public class DeleteAuthor : BaseEndpointWithoutResponse<DeleteAuthorRequest>
{
    private readonly IAuthorService _Author;

    public DeleteAuthor(IAuthorService Author)
    {
        _Author = Author;
    }

    [HttpDelete("{Id}")]
    //[Authorize]
    //[RequiredScope(typeof(AuthorScope))]
    [SwaggerOperation(
        Summary = "Remove data from Author by Id",
        Description = "",
        OperationId = "Author.DeleteAuthor",
        Tags = new[] { "Author" })
    ]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public override async Task<ActionResult> HandleAsync([FromRoute] DeleteAuthorRequest request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        await _Author.DeleteAsync(request.Id);
        return Ok();
    }
}