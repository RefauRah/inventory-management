using InventoryManagement.Domain.Entities;
using InventoryManagement.Shared.Abstractions.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Persistence.Postgres.Configurations;

public class InventoryConfiguration : BaseEntityConfiguration<Inventory>
{
    protected override void EntityConfiguration(EntityTypeBuilder<Inventory> builder)
    {
        builder.HasKey(e => new { e.Id, e.BookId });
        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.BookId).
            HasMaxLength(100);        
    }
}