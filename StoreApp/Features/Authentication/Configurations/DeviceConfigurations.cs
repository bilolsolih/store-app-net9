using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Features.Authentication.Models;

namespace StoreApp.Features.Authentication.Configurations;

public class DeviceConfigurations : IEntityTypeConfiguration<Device>
{
  public void Configure(EntityTypeBuilder<Device> builder)
  {
    builder.ToTable("devices");
    builder.HasKey(d => d.Id);

    builder.HasOne(d => d.User)
      .WithMany(u => u.Devices)
      .HasForeignKey(d => d.UserId);

    builder.HasIndex(d => d.FcmToken)
      .IsUnique();

    builder.Property(d => d.Id)
      .HasColumnName("id");


    builder.Property(d => d.UserId)
      .HasColumnName("user_id")
      .IsRequired();

    builder.Property(d => d.FcmToken)
      .HasColumnName("fcm_token")
      .IsRequired()
      .HasMaxLength(4);

    builder.Property(d => d.Created)
      .HasColumnName("created")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();

    builder.Property(d => d.Updated)
      .HasColumnName("updated")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();
  }
}