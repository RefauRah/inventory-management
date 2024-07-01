namespace InventoryManagement.WebApi.Contracts.Responses;

public record BookCategoryResponse
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
}