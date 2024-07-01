using InventoryManagement.Domain.Entities;
using InventoryManagement.Shared.Abstractions.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Persistence.Postgres.Configurations;

public class BookConfiguration : BaseEntityConfiguration<Book>
{
    protected override void EntityConfiguration(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(e => new { e.Id, e.AuthorId, e.CategoryId, e.PublisherId });
        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.AuthorId)
            .HasMaxLength(100);
        builder.Property(e => e.CategoryId)
            .HasMaxLength(100);
        builder.Property(e => e.PublisherId)
            .HasMaxLength(100);
        builder.Property(e => e.Title)
            .HasMaxLength(100);
        builder.Property(e => e.Description)
            .HasMaxLength(512);
        builder.Property(e => e.Isbn)
            .HasMaxLength(20);
        builder.Property(e => e.Dimensions)
            .HasMaxLength(20);
        builder.Property(e => e.Cover)
            .HasMaxLength(256);
        builder.Property(e => e.Language)
            .HasMaxLength(50);
    }
}