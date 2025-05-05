using System.Text.Json.Serialization;
using StoreApp.Features.Cart.Models;
using StoreApp.Features.Products.Models;

namespace StoreApp.Features.Authentication.Models;

public enum Gender
{
  Male,
  Female
}

public class User
{
  public int Id { get; set; }
  public required string FullName { get; set; }
  public required string Email { get; set; }
  public required string Password { get; set; }

  public DateOnly? BirthDate { get; set; }
  public Gender? Gender { get; set; }
  public string? PhoneNumber { get; set; }

  public ICollection<Product> SavedProducts { get; set; } = [];
  public ICollection<Device> Devices { get; set; } = [];
  public UserCart Cart { get; set; }

  [JsonIgnore]
  public ICollection<Otp> Otps { get; set; } = [];

  public DateTime Created { get; set; }
  public DateTime Updated { get; set; }
}