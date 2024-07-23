using InventoryManagement.Core.Abstractions;
using InventoryManagement.Shared.Abstractions.Databases;
using InventoryManagement.Shared.Abstractions.Encryption;
using InventoryManagement.WebApi.Endpoints.BookCategory.Requests;
using InventoryManagement.WebApi.Mapping;
using InventoryManagement.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryManagement.WebApi.Endpoints.BookCategory;

public class CreateBookCategory : BaseEndpointWithoutResponse<CreateBookCategoryRequest>
{
    private readonly IDbContext _dbContext;
    private readonly IBookCategoryService _bookCategoryService;
    private readonly IRng _rng;
    private readonly ISalter _salter;
    private readonly IStringLocalizer<CreateBookCategory> _localizer;

    public CreateBookCategory(IDbContext dbContext,
        IBookCategoryService BookCategoryService,
        IRng rng,
        ISalter salter,
        IStringLocalizer<CreateBookCategory> localizer)
    {
        _dbContext = dbContext;
        _bookCategoryService = BookCategoryService;
        _rng = rng;
        _salter = salter;
        _localizer = localizer;
    }

    [HttpPost]
    //[Authorize]
    //[RequiredScope(typeof(BookCategoryScope))]
    [SwaggerOperation(
        Summary = "Create Book Category",
        Description = "",
        OperationId = "BookCategory.CreateBookCategory",
        Tags = new[] { "BookCategory" })
    ]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult> HandleAsync(CreateBookCategoryRequest request,
        CancellationToken cancellationToken = new())
    {
        var validator = new CreateBookCategoryRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(Error.Create(_localizer["invalid-parameter"], validationResult.Construct()));

        var BookCategoryExist = await _bookCategoryService.IsBookCategoryExistAsync(request.Name!, cancellationToken);
        if (BookCategoryExist)
            return BadRequest(Error.Create(_localizer["name-exists"]));

        var BookCategory = request.ToBookCategory(
            _rng.Generate(128, false),
            _salter);

        await _dbContext.InsertAsync(BookCategory, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}