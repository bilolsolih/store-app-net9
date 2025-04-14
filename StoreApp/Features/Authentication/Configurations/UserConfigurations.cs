using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Features.Authentication.Models;

namespace StoreApp.Features.Authentication.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("users");
    builder.HasKey(u => u.Id);

    builder.HasMany(u => u.SavedProducts)
      .WithMany();

    builder.HasIndex(u => u.Email)
      .IsUnique();

    builder.HasIndex(u => u.PhoneNumber)
      .IsUnique();

    builder.Property(u => u.Id)
      .HasColumnName("id");


    builder.Property(u => u.FullName)
      .HasColumnName("full_name")
      .IsRequired()
      .HasMaxLength(64);

    builder.Property(u => u.Email)
      .HasColumnName("email")
      .IsRequired()
      .HasMaxLength(64);


    builder.Property(u => u.PhoneNumber)
      .HasColumnName("phone_number")
      .IsRequired(false)
      .HasMaxLength(16);

    builder.Property(u => u.BirthDate)
      .HasColumnName("birth_date")
      .IsRequired(false);

    builder.Property(u => u.Password)
      .HasColumnName("password")
      .IsRequired();

    builder.Property(u => u.Gender)
      .HasColumnName("gender")
      .IsRequired(false);


    builder.Property(u => u.Created)
      .HasColumnName("created")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();

    builder.Property(u => u.Updated)
      .HasColumnName("updated")
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAddOrUpdate();
  }
}