using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Features.Authentication.Models;

namespace StoreApp.Features.Authentication.Configurations;

public class CardConfigurations : IEntityTypeConfiguration<Card>
{
  public void Configure(EntityTypeBuilder<Card> builder)
  {
    builder.ToTable("cards");
    builder.HasKey(obj => obj.Id);
    builder.HasIndex(obj => new { obj.UserId, obj.CardNumber }).IsUnique();

    builder.HasOne(obj => obj.User)
      .WithMany(u => u.Cards)
      .HasForeignKey(obj => obj.UserId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.Property(obj => obj.Id)
      .HasColumnName("id");

    builder.Property(obj => obj.UserId)
      .HasColumnName("user_id")
      .IsRequired();

    builder.Property(obj => obj.CardNumber)
      .HasColumnName("card_number")
      .IsRequired()
      .HasMaxLength(16);

    builder.Property(obj => obj.ExpiryDate)
      .HasColumnName("expiry_date")
      .IsRequired();

    builder.Property(obj => obj.SecurityCode)
      .HasColumnName("security_code")
      .IsRequired()
      .HasMaxLength(3);

    builder.Property(obj => obj.Created)
      .HasColumnName("created")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd()
      .IsRequired();

    builder.Property(obj => obj.Updated)
      .HasColumnName("updated")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd()
      .IsRequired();
  }
}