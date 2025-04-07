using Microsoft.EntityFrameworkCore;
using StoreApp.Features.Authentication.Configurations;
using StoreApp.Features.Authentication.Models;

namespace StoreApp;

public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
{
  public DbSet<User> Users { get; set; }
  public DbSet<Otp> Otps { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfiguration(new UserConfigurations());
    modelBuilder.ApplyConfiguration(new OtpConfigurations());
  }
}