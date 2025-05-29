using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Features.Authentication.Models;

namespace StoreApp.Features.Authentication.Configurations;

public class AddressConfigurations : IEntityTypeConfiguration<Address>
{
  public void Configure(EntityTypeBuilder<Address> builder)
  {
    builder.ToTable("addresses");
    builder.HasKey(a => a.Id);

    builder.HasOne(a => a.User)
      .WithMany(u => u.Addresses)
      .HasForeignKey(a => a.UserId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.Property(a => a.Id)
      .HasColumnName("id")
      .IsRequired();


    builder.Property(a => a.Title)
      .HasColumnName("title")
      .HasMaxLength(64)
      .IsRequired();

    builder.Property(a => a.UserId)
      .HasColumnName("user_id")
      .IsRequired();

    builder.Property(a => a.IsDefault)
      .HasColumnName("is_default")
      .IsRequired();

    builder.Property(a => a.FullAddress)
      .HasColumnName("full_address")
      .HasMaxLength(128)
      .IsRequired();

    builder.Property(a => a.Lat)
      .HasColumnName("lat")
      .IsRequired();

    builder.Property(a => a.Lng)
      .HasColumnName("lng")
      .IsRequired();

    builder.Property(a => a.Created)
      .HasColumnName("created")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd()
      .IsRequired();

    builder.Property(a => a.Updated)
      .HasColumnName("updated")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd()
      .IsRequired();
  }
}