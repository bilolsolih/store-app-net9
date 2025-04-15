using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Features.Products.Models;
using StoreApp.Features.Reviews.Models;

namespace StoreApp.Features.Reviews.Configurations;

public class ReviewConfigurations : IEntityTypeConfiguration<Review>
{
  public void Configure(EntityTypeBuilder<Review> builder)
  {
    builder.ToTable("reviews");
    builder.HasKey(r => r.Id);

    builder.HasOne(r => r.User)
      .WithMany()
      .HasForeignKey(r => r.UserId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne<Product>()
      .WithMany(p => p.Reviews)
      .HasForeignKey(r => r.ProductId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasIndex(r => new { r.UserId, r.ProductId }).IsUnique();

    builder.Property(r => r.Id)
      .HasColumnName("id");

    builder.Property(r => r.Comment)
      .HasColumnName("comment")
      .IsRequired()
      .HasMaxLength(256);

    builder.Property(r => r.Rating)
      .HasColumnName("rating")
      .IsRequired();

    builder.Property(r => r.UserId)
      .HasColumnName("user_id")
      .IsRequired();

    builder.Property(r => r.ProductId)
      .HasColumnName("product_id")
      .IsRequired();


    builder.Property(r => r.Created)
      .HasColumnName("created")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();

    builder.Property(r => r.Updated)
      .HasColumnName("updated")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();
  }
}