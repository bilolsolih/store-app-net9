using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Features.Notifications.Models;

namespace StoreApp.Features.Notifications.Configurations;

public class NotificationConfigurations : IEntityTypeConfiguration<Notification>
{
  public void Configure(EntityTypeBuilder<Notification> builder)
  {
    builder.ToTable("notifications");
    builder.HasKey(n => n.Id);

    builder.HasOne(n => n.NotificationType)
      .WithMany()
      .HasForeignKey(n => n.NotificationTypeId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasMany(n => n.SentDevices)
      .WithMany(d => d.ReceivedNotifications);

    builder.Property(n => n.Id)
      .HasColumnName("id");

    builder.Property(n => n.Title)
      .HasColumnName("title")
      .HasMaxLength(32)
      .IsRequired();

    builder.Property(n => n.Body)
      .HasColumnName("body")
      .HasMaxLength(128)
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