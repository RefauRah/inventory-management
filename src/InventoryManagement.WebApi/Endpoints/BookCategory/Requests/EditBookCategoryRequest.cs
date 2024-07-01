using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.WebApi.Endpoints.BookCategory.Requests
{
    public class EditBookCategoryRequest
    {
        [FromRoute] public Guid Id { get; set; }
        [FromBody] public EditBookCategoryRequestPayload Payload { get; set; } = null!;
    }
}