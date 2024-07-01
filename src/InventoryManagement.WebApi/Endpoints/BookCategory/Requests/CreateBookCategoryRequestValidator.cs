using InventoryManagement.WebApi.Validators;
using FluentValidation;

namespace InventoryManagement.WebApi.Endpoints.BookCategory.Requests;

public class CreateBookCategoryRequestValidator : AbstractValidator<CreateBookCategoryRequest>
{
    public CreateBookCategoryRequestValidator()
    {
        RuleFor(e => e.Name).NotNull().NotEmpty().MaximumLength(100).SetValidator(new AsciiOnlyValidator());
    }
}