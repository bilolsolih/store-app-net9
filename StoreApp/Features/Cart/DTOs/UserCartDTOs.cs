namespace StoreApp.Features.Cart.DTOs;

public record MyCartDto
{
  public required List<CartItemListDto> Items { get; set; }
  public required double SubTotal { get; set; }
  public required double VAT { get; set; }
  public required double ShippingFee { get; set; }
  public required double Total { get; set; }
}