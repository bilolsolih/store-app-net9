using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Features.Products.Models;

namespace StoreApp.Features.Products.Configurations;

public class SizeConfigurations : IEntityTypeConfiguration<Size>
{
  public void Configure(EntityTypeBuilder<Size> builder)
  {
    builder.ToTable("sizes");
    builder.HasKey(s => s.Id);
    builder.HasIndex(s => s.Title)
      .IsUnique();

    builder.Property(s => s.Id)
      .HasColumnName("id");

    builder.Property(s => s.Title)
      .HasColumnName("title")
      .IsRequired()
      .HasMaxLength(32);

    builder.Property(s => s.Description)
      .HasColumnName("description")
      .IsRequired(false)
      .HasMaxLength(256);

    builder.Property(s => s.Created)
      .HasColumnName("created")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();

    builder.Property(s => s.Updated)
      .HasColumnName("updated")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();
  }
}