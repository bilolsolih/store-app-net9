using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Features.Authentication.Models;
using StoreApp.Features.Cart.Models;

namespace StoreApp.Features.Cart.Configurations;

public class UserCartConfigurations : IEntityTypeConfiguration<UserCart>
{
  public void Configure(EntityTypeBuilder<UserCart> builder)
  {
    builder.ToTable("carts");
    builder.HasKey(c => c.Id);

    builder.HasOne(c => c.User)
      .WithOne(u => u.Cart)
      .HasForeignKey<UserCart>(c => c.UserId);

    builder.Property(c => c.Id)
      .HasColumnName("id");

    builder.Property(c => c.UserId)
      .HasColumnName("user_id")
      .IsRequired();

    builder.Property(c => c.Created)
      .HasColumnName("created")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();

    builder.Property(c => c.Updated)
      .HasColumnName("updated")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAddOrUpdate();
  }
}