using InventoryManagement.WebApi.Validators;
using FluentValidation;

namespace InventoryManagement.WebApi.Endpoints.BookCategory.Requests;

public class EditBookCategoryRequestPayloadValidator : AbstractValidator<EditBookCategoryRequestPayload>
{
    public EditBookCategoryRequestPayloadValidator()
    {
        RuleFor(e => e.Name).NotNull();

        When(e => !string.IsNullOrWhiteSpace(e.Name),
            () => { RuleFor(e => e.Name).SetValidator(new AsciiOnlyValidator()!).MaximumLength(100); });

        }
}