using System.Text.Json.Serialization;
using StoreApp.Core;
using StoreApp.Features.Authentication.Models;

namespace StoreApp.Features.Cart.Models;

public class UserCart : BaseModel
{
  public required int UserId { get; set; }
  [JsonIgnore]
  public User User { get; set; }
  [JsonIgnore]
  public ICollection<CartItem> CartItems { get; set; } = [];
}