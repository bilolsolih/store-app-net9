using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Features.Products.Models;

namespace StoreApp.Features.Products.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.ToTable("products");
    builder.HasKey(p => p.Id);
    
    builder.HasOne(p => p.Category)
      .WithMany(c => c.Products)
      .HasForeignKey(p => p.CategoryId)
      .OnDelete(DeleteBehavior.Restrict);
    

    builder.HasMany(p => p.Sizes)
      .WithMany(s => s.Products)
      .UsingEntity(typeBuilder => typeBuilder.ToTable("product_to_size"));

    builder.Property(p => p.Id)
      .HasColumnName("id");
    
    builder.Property(p => p.CategoryId)
      .HasColumnName("category_id")
      .IsRequired();
    
    builder.Property(p => p.Title)
      .HasColumnName("title")
      .IsRequired()
      .HasMaxLength(64);
    
    builder.Property(p => p.Description)
      .HasColumnName("description")
      .IsRequired()
      .HasMaxLength(256);
    
    builder.Property(p => p.Price)
      .HasColumnName("price")
      .IsRequired();
    
    builder.Property(p => p.Created)
      .HasColumnName("created")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();

    builder.Property(p => p.Updated)
      .HasColumnName("updated")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();
  }
}