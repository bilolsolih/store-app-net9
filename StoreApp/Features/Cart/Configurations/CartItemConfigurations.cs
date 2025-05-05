using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Features.Cart.Models;

namespace StoreApp.Features.Cart.Configurations;

public class CartItemConfigurations : IEntityTypeConfiguration<CartItem>
{
  public void Configure(EntityTypeBuilder<CartItem> builder)
  {
    builder.ToTable("cart_items");
    builder.HasKey(c => c.Id);

    builder.HasOne(c => c.Product)
      .WithMany()
      .HasForeignKey(c => c.ProductId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(c => c.Size)
      .WithMany()
      .HasForeignKey(c => c.SizeId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.Property(c => c.Id)
      .HasColumnName("id");

    builder.Property(c => c.ProductId)
      .HasColumnName("product_id")
      .IsRequired();

    builder.Property(c => c.SizeId)
      .HasColumnName("size_id")
      .IsRequired();

    builder.Property(c => c.Quantity)
      .HasColumnName("quantity")
      .IsRequired();

    builder.Property(c => c.Created)
      .HasColumnName("created")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd()
      .IsRequired();

    builder.Property(c => c.Updated)
      .HasColumnName("updated")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd()
      .IsRequired();
  }
}