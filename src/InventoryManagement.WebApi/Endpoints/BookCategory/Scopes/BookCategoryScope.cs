using InventoryManagement.WebApi.Scopes;

namespace InventoryManagement.WebApi.Endpoints.BookCategory.Scopes;

public class BookCategoryScope : IScope
{
    public string ScopeName => nameof(BookCategoryScope).ToLower();
}