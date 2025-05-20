using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Features.Orders.Models;

namespace StoreApp.Features.Orders.Configurations;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.ToTable("orders");

    builder.HasKey(o => o.Id);

    builder.HasOne(o => o.User)
      .WithMany()
      .HasForeignKey(o => o.UserId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(o => o.Address)
      .WithMany()
      .HasForeignKey(o => o.AddressId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(o => o.Card)
      .WithMany()
      .HasForeignKey(o => o.CardId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.Property(o => o.Id)
      .HasColumnName("id");

    builder.Property(o => o.AddressId)
      .HasColumnName("address_id")
      .IsRequired();

    builder.Property(o => o.PaymentMethod)
      .HasColumnName("payment_method")
      .IsRequired();

    builder.Property(o => o.CardId)
      .HasColumnName("card_id")
      .IsRequired(false);

    builder.Property(o => o.SubTotal)
      .HasColumnName("sub_total")
      .IsRequired();

    builder.Property(o => o.VAT)
      .HasColumnName("vat")
      .IsRequired();

    builder.Property(o => o.ShippingFee)
      .HasColumnName("shipping_fee")
      .IsRequired();

    builder.Property(o => o.Total)
      .HasColumnName("total")
      .IsRequired();

    builder.Property(o => o.Status)
      .HasColumnName("status")
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