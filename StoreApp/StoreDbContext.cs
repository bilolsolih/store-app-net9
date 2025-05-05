using Microsoft.EntityFrameworkCore;
using StoreApp.Features.Authentication.Configurations;
using StoreApp.Features.Authentication.Models;
using StoreApp.Features.Cart.Configurations;
using StoreApp.Features.Cart.Models;
using StoreApp.Features.Notifications.Configurations;
using StoreApp.Features.Notifications.Models;
using StoreApp.Features.Products.Configurations;
using StoreApp.Features.Products.Models;
using StoreApp.Features.Reviews.Configurations;
using StoreApp.Features.Reviews.Models;

namespace StoreApp;

public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
{
  public DbSet<User> Users { get; set; }
  public DbSet<Device> Devices { get; set; }
  public DbSet<Otp> Otps { get; set; }

  public DbSet<Category> Categories { get; set; }
  public DbSet<Size> Sizes { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<ProductImage> ProductImages { get; set; }
  public DbSet<Review> Reviews { get; set; }

  public DbSet<NotificationType> NotificationTypes { get; set; }
  public DbSet<Notification> Notifications { get; set; }

  public DbSet<UserCart> UserCarts { get; set; }
  public DbSet<CartItem> CartItems { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfiguration(new UserConfigurations());
    modelBuilder.ApplyConfiguration(new DeviceConfigurations());
    modelBuilder.ApplyConfiguration(new OtpConfigurations());
    modelBuilder.ApplyConfiguration(new CategoryConfigurations());
    modelBuilder.ApplyConfiguration(new SizeConfigurations());
    modelBuilder.ApplyConfiguration(new ProductConfigurations());
    modelBuilder.ApplyConfiguration(new ProductImageConfigurations());
    modelBuilder.ApplyConfiguration(new ReviewConfigurations());

    modelBuilder.ApplyConfiguration(new NotificationTypeConfigurations());
    modelBuilder.ApplyConfiguration(new NotificationConfigurations());

    modelBuilder.ApplyConfiguration(new UserCartConfigurations());
    modelBuilder.ApplyConfiguration(new CartItemConfigurations());
  }
}