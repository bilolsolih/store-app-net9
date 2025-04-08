using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Features.Products.Models;

namespace StoreApp.Features.Products.Configurations;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
  public void Configure(EntityTypeBuilder<Category> builder)
  {
    builder.ToTable("categories");
    builder.HasKey(c => c.Id);
    
    builder.HasIndex(c => c.Title)
      .IsUnique();

    builder.Property(c => c.Id)
      .HasColumnName("id");
    
    builder.Property(c => c.Title)
      .HasColumnName("title")
      .IsRequired()
      .HasMaxLength(32);
    
    builder.Property(c => c.Created)
      .HasColumnName("created")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();
    
    builder.Property(c => c.Updated)
      .HasColumnName("updated")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();

  }
}