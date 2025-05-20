using Microsoft.EntityFrameworkCore;
using StoreApp.Features.Authentication.Configurations;
using StoreApp.Features.Authentication.Models;
using StoreApp.Features.Cart.Configurations;
using StoreApp.Features.Cart.Models;
using StoreApp.Features.Notifications.Configurations;
using StoreApp.Features.Notifications.Models;
using StoreApp.Features.Orders.Configurations;
using StoreApp.Features.Orders.Models;
using StoreApp.Features.Products.Configurations;
using StoreApp.Features.Products.Models;
using StoreApp.Features.Reviews.Configurations;
using StoreApp.Features.Reviews.Models;

namespace StoreApp;

public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
{
  public DbSet<User> Users { get; set; }
  public DbSet<Address> Addresses { get; set; }
  public DbSet<Device> Devices { get; set; }
  public DbSet<Otp> Otps { get; set; }
  public DbSet<Card> Cards { get; set; }

  public DbSet<Category> Categories { get; set; }
  public DbSet<Size> Sizes { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<ProductImage> ProductImages { get; set; }
  public DbSet<Review> Reviews { get; set; }

  public DbSet<NotificationType> NotificationTypes { get; set; }
  public DbSet<Notification> Notifications { get; set; }

  public DbSet<UserCart> UserCarts { get; set; }
  public DbSet<CartItem> CartItems { get; set; }

  public DbSet<OrderItem> OrderItems { get; set; }
  public DbSet<Order> Orders { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    builder.ApplyConfiguration(new UserConfigurations());
    builder.ApplyConfiguration(new AddressConfigurations());
    builder.ApplyConfiguration(new DeviceConfigurations());
    builder.ApplyConfiguration(new OtpConfigurations());
    builder.ApplyConfiguration(new CardConfigurations());

    builder.ApplyConfiguration(new CategoryConfigurations());
    builder.ApplyConfiguration(new SizeConfigurations());
    builder.ApplyConfiguration(new ProductConfigurations());
    builder.ApplyConfiguration(new ProductImageConfigurations());
    builder.ApplyConfiguration(new ReviewConfigurations());

    builder.ApplyConfiguration(new NotificationTypeConfigurations());
    builder.ApplyConfiguration(new NotificationConfigurations());

    builder.ApplyConfiguration(new UserCartConfigurations());
    builder.ApplyConfiguration(new CartItemConfigurations());

    builder.ApplyConfiguration(new OrderItemConfigurations());
    builder.ApplyConfiguration(new OrderConfigurations());
  }
}