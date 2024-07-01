using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.WebApi.Endpoints.BookCategory.Requests
{
    public class DeleteBookCategoryRequest
    {
        [FromRoute] public Guid Id { get; set; }
    }
}