using InventoryManagement.Core.Abstractions;
using InventoryManagement.Shared.Abstractions.Databases;
using InventoryManagement.WebApi.Endpoints.BookCategory.Requests;
using InventoryManagement.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryManagement.WebApi.Endpoints.BookCategory;

public class EditBookCategory : BaseEndpointWithoutResponse<EditBookCategoryRequest>
{
    private readonly IBookCategoryService _bookCategoryService;
    private readonly IDbContext _dbContext;
    private readonly IStringLocalizer<EditBookCategory> _localizer;

    public EditBookCategory(IBookCategoryService bookCategoryService,
        IDbContext dbContext,
        IStringLocalizer<EditBookCategory> localizer)
    {
        _bookCategoryService = bookCategoryService;
        _dbContext = dbContext;
        _localizer = localizer;
    }

    [HttpPut("{Id}")]
    //[Authorize]
    //[RequiredScope(typeof(BookCategoryScope))]
    [SwaggerOperation(
        Summary = "Edit Book Category",
        Description = "",
        OperationId = "BookCategory.EditBookCategory",
        Tags = new[] { "BookCategory" })
    ]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult> HandleAsync([FromRoute] EditBookCategoryRequest request,
        CancellationToken cancellationToken = new())
    {
        var validator = new EditBookCategoryRequestPayloadValidator();
        var validationResult = await validator.ValidateAsync(request.Payload, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(Error.Create(_localizer["invalid-parameter"], validationResult.Construct()));

        var BookCategory = await _bookCategoryService.GetByExpressionAsync(
            e => e.Id == request.Id,
            e => new InventoryManagement.Domain.Entities.BookCategory
            {
                Id = e.Id,
                Name = e.Name                
            }, cancellationToken);

        if (BookCategory is null)
            return BadRequest(Error.Create(_localizer["data-not-found"]));

        _dbContext.AttachEntity(BookCategory);

        if (request.Payload.Name != BookCategory.Name)
            BookCategory.Name = request.Payload.Name!;
       
        await _dbContext.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}