using System.ComponentModel.DataAnnotations.Schema;
using StoreApp.Core;
using StoreApp.Features.Authentication.Models;

namespace StoreApp.Features.Orders.Models;

public enum PaymentMethods
{
  Card,
  Cash
}

public enum OrderStatus
{
  Packing,
  Picked,
  Canceled,
  Completed,
}

public class Order : BaseModel
{
  public required int UserId { get; set; }
  public User User { get; set; }

  public required int AddressId { get; set; }
  public Address Address { get; set; }

  public required PaymentMethods PaymentMethod { get; set; }
  public int? CardId { get; set; }
  public Card Card { get; set; }

  public required double SubTotal { get; set; }
  public required double VAT { get; set; }
  public required double ShippingFee { get; set; }
  public required double Total { get; set; }

  public required OrderStatus Status { get; set; }

  public ICollection<OrderItem> OrderItems { get; set; } = [];
}