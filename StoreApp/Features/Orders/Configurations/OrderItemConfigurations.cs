using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Features.Orders.Models;

namespace StoreApp.Features.Orders.Configurations;

public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
{
  public void Configure(EntityTypeBuilder<OrderItem> builder)
  {
    builder.ToTable("order_items");

    builder.HasKey(o => o.Id);

    // builder.HasOne(o => o.Order)
    //   .WithMany()
    //   .HasForeignKey(o => o.OrderId);

    builder.HasOne(o => o.Product)
      .WithMany()
      .HasForeignKey(o => o.ProductId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(o => o.Size)
      .WithMany()
      .HasForeignKey(o => o.SizeId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.Property(o => o.Id)
      .HasColumnName("id");

    // builder.Property(o => o.OrderId)
    //   .HasColumnName("order_id")
    //   .IsRequired();

    builder.Property(o => o.ProductId)
      .HasColumnName("product_id")
      .IsRequired();

    builder.Property(o => o.SizeId)
      .HasColumnName("size_id")
      .IsRequired();

    builder.Property(o => o.Quantity)
      .HasColumnName("quantity")
      .IsRequired();

    builder.Property(o => o.PriceTotal)
      .HasColumnName("price_total")
      .IsRequired();

    builder.Property(o => o.Created)
      .HasColumnName("created")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd()
      .IsRequired();

    builder.Property(o => o.Updated)
      .HasColumnName("updated")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd()
      .IsRequired();
  }
}