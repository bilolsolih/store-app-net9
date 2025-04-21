using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Features.Notifications.Models;

namespace StoreApp.Features.Notifications.Configurations;

public class NotificationTypeConfigurations : IEntityTypeConfiguration<NotificationType>
{
  public void Configure(EntityTypeBuilder<NotificationType> builder)
  {
    builder.ToTable("notification_types");
    builder.HasKey(n => n.Id);

    builder.HasIndex(n => n.Title)
      .IsUnique();

    builder.Property(n => n.Id)
      .HasColumnName("id");

    builder.Property(n => n.Title)
      .HasColumnName("title")
      .HasMaxLength(32)
      .IsRequired();

    builder.Property(n => n.Icon)
      .HasColumnName("icon")
      .HasMaxLength(32)
      .IsRequired();

    builder.Property(n => n.Created)
      .HasColumnName("created")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();

    builder.Property(n => n.Updated)
      .HasColumnName("updated")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();
  }
}