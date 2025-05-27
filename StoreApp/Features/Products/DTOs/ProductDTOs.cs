namespace StoreApp.Features.Products.DTOs;

public record ProductListDto
{
  public required int Id { get; set; }
  public required int CategoryId { get; set; }
  public string? Image { get; set; }
  public required string Title { get; set; }
  public required double Price { get; set; }
  public required bool IsLiked { get; set; }
  public required double Discount { get; set; }
}

public record Picture
{
  public required int Id { get; set; }
  public required string Image { get; set; }
}

public record ProductSize
{
  public required int Id { get; set; }
  public required string Title { get; set; }
}

public record ProductDetailDto
{
  public required int Id { get; set; }
  public required string Title { get; set; }
  public required string Description { get; set; }
  public required double Price { get; set; }
  public required bool IsLiked { get; set; }
  public required List<Picture> ProductImages { get; set; }
  public required List<ProductSize> ProductSizes { get; set; }
  public required int? ReviewsCount { get; set; }
  public required double? Rating { get; set; }
}