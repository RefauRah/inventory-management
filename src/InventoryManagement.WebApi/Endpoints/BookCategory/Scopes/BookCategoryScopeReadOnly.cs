using InventoryManagement.WebApi.Scopes;

namespace InventoryManagement.WebApi.Endpoints.BookCategory.Scopes;

public class BookCategoryScopeReadOnly : IScope
{
    public string ScopeName => $"{nameof(BookCategoryScope)}.readonly".ToLower();
}