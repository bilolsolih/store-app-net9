using StoreApp.Features.Authentication.Models;

namespace StoreApp.Features.Orders.Models;

public enum PaymentMethods
{
  Card,
  Cash
}

public class Order
{
  public required int AddressId { get; set; }
  public Address Address { get; set; }

  public required PaymentMethods PaymentMethod { get; set; }
  public int? CardId { get; set; }
  public Card Card { get; set; }

  public required double SubTotal { get; set; }
  public required double VAT { get; set; }
  public required double ShippingFee { get; set; }
  public required double Total { get; set; }
}