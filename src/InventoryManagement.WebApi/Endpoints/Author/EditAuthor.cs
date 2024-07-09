using InventoryManagement.Core.Abstractions;
using InventoryManagement.Shared.Abstractions.Databases;
using InventoryManagement.WebApi.Endpoints.Author.Requests;
using InventoryManagement.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryManagement.WebApi.Endpoints.Author;

public class EditAuthor : BaseEndpointWithoutResponse<EditAuthorRequest>
{
    private readonly IAuthorService _AuthorService;
    private readonly IDbContext _dbContext;
    private readonly IStringLocalizer<EditAuthor> _localizer;

    public EditAuthor(IAuthorService AuthorService,
        IDbContext dbContext,
        IStringLocalizer<EditAuthor> localizer)
    {
        _AuthorService = AuthorService;
        _dbContext = dbContext;
        _localizer = localizer;
    }

    [HttpPut("{Id}")]
    //[Authorize]
    //[RequiredScope(typeof(AuthorScope))]
    [SwaggerOperation(
        Summary = "Edit Author",
        Description = "",
        OperationId = "Author.EditAuthor",
        Tags = new[] { "Author" })
    ]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult> HandleAsync([FromRoute] EditAuthorRequest request,
        CancellationToken cancellationToken = new())
    {
        var validator = new EditAuthorRequestPayloadValidator();
        var validationResult = await validator.ValidateAsync(request.Payload, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(Error.Create(_localizer["invalid-parameter"], validationResult.Construct()));

        var Author = await _AuthorService.GetByExpressionAsync(
            e => e.Id == request.Id,
            e => new InventoryManagement.Domain.Entities.Author
            {
                Id = e.Id,
                Name = e.Name,
                Biography = e.Biography,
                Image = e.Image,
            }, cancellationToken);

        if (Author is null)
            return BadRequest(Error.Create(_localizer["data-not-found"]));

        _dbContext.AttachEntity(Author);

        if (request.Payload.Name != Author.Name)
            Author.Name = request.Payload.Name!;
        
        if (request.Payload.Biography != Author.Biography)
            Author.Biography = request.Payload.Biography!;
        
        //if (request.Payload.Image != Author.Image)
        //    Author.Image = request.Payload.Image!;
       
        await _dbContext.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}