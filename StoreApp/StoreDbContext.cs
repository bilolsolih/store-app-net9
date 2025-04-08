using Microsoft.EntityFrameworkCore;
using StoreApp.Features.Authentication.Configurations;
using StoreApp.Features.Authentication.Models;
using StoreApp.Features.Products.Configurations;
using StoreApp.Features.Products.Models;

namespace StoreApp;

public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
{
  public DbSet<User> Users { get; set; }
  public DbSet<Otp> Otps { get; set; }

  public DbSet<Category> Categories { get; set; }
  public DbSet<Size> Sizes { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<ProductImage> ProductImages { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfiguration(new UserConfigurations());
    modelBuilder.ApplyConfiguration(new OtpConfigurations());
    modelBuilder.ApplyConfiguration(new CategoryConfigurations());
    modelBuilder.ApplyConfiguration(new SizeConfigurations());
    modelBuilder.ApplyConfiguration(new ProductConfigurations());
    modelBuilder.ApplyConfiguration(new ProductImageConfigurations());
  }
}