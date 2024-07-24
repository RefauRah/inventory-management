using InventoryManagement.Core.Abstractions;
using InventoryManagement.Shared.Abstractions.Databases;
using InventoryManagement.Shared.Abstractions.Encryption;
using InventoryManagement.WebApi.Endpoints.Book.Requests;
using InventoryManagement.WebApi.Mapping;
using InventoryManagement.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryManagement.WebApi.Endpoints.Book;

public class CreateBook : BaseEndpointWithoutResponse<CreateBookRequest>
{
    private readonly IDbContext _dbContext;
    private readonly IBookService _bookService;
    private readonly IRng _rng;
    private readonly ISalter _salter;
    private readonly IStringLocalizer<CreateBook> _localizer;

    public CreateBook(IDbContext dbContext,
        IBookService BookService,
        IRng rng,
        ISalter salter,
        IStringLocalizer<CreateBook> localizer)
    {
        _dbContext = dbContext;
        _bookService = BookService;
        _rng = rng;
        _salter = salter;
        _localizer = localizer;
    }

    [HttpPost]
    //[Authorize]
    //[RequiredScope(typeof(BookScope))]
    [SwaggerOperation(
        Summary = "Create Book ",
        Description = "",
        OperationId = "Book.CreateBook",
        Tags = new[] { "Book" })
    ]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public override async Task<ActionResult> HandleAsync(CreateBookRequest request,
        CancellationToken cancellationToken = new())
    {
        var validator = new CreateBookRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(Error.Create(_localizer["invalid-parameter"], validationResult.Construct()));

        var BookExist = await _bookService.IsBookExistAsync(request.Title!, cancellationToken);
        if (BookExist)
            return BadRequest(Error.Create(_localizer["name-exists"]));

        var Book = request.ToBook(
            _rng.Generate(128, false),
            _salter);

        await _dbContext.InsertAsync(Book, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}