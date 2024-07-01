using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.WebApi.Endpoints.BookCategory.Requests;

public class GetAllBookCategoryRequest
{
    [FromQuery(Name = "s")] public string? Search { get; set; }
}