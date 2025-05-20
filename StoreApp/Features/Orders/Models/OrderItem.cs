using StoreApp.Core;
using StoreApp.Features.Products.Models;

namespace StoreApp.Features.Orders.Models;

public class OrderItem : BaseModel
{
  public required int OrderId { get; set; }
  public Order Order { get; set; }

  public required int ProductId { get; set; }
  public Product Product { get; set; }

  public required int SizeId { get; set; }
  public Size Size { get; set; }

  public required int Quantity { get; set; }

  public required double PriceTotal { get; set; }
}