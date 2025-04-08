using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Features.Products.Models;

namespace StoreApp.Features.Products.Configurations;

public class ProductImageConfigurations : IEntityTypeConfiguration<ProductImage>
{
  public void Configure(EntityTypeBuilder<ProductImage> builder)
  {
    builder.ToTable("product_images");
    builder.HasKey(c => c.Id);

    builder.HasIndex(c => c.Image)
      .IsUnique();

    builder.HasOne(pi => pi.Product)
      .WithMany(p => p.ProductImages)
      .HasForeignKey(pi => pi.ProductId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.Property(c => c.Id)
      .HasColumnName("id");

    builder.Property(c => c.ProductId)
      .HasColumnName("product_id");

    builder.Property(c => c.Image)
      .HasColumnName("image")
      .IsRequired()
      .HasMaxLength(64);

    builder.Property(c => c.IsMain)
      .HasColumnName("is_main")
      .IsRequired();

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