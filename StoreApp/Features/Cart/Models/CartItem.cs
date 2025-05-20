using Newtonsoft.Json;
using StoreApp.Core;
using StoreApp.Features.Orders.Models;
using StoreApp.Features.Products.Models;

namespace StoreApp.Features.Cart.Models;

public class CartItem : BaseModel
{
  public required int UserCartId { get; set; }

  [JsonIgnore]
  public UserCart UserCart { get; set; }

  public required int ProductId { get; set; }

  [JsonIgnore]
  public Product Product { get; set; }

  public required int SizeId { get; set; }

  [JsonIgnore]
  public Size Size { get; set; }

  public required int Quantity { get; set; }
}