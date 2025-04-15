using StoreApp.Features.Reviews.Models;

namespace StoreApp.Features.Products.Models;

public class Product
{
  public int Id { get; set; }

  public required Category Category { get; set; }
  public required int CategoryId { get; set; }

  public required string Title { get; set; }
  public required string Description { get; set; }
  public required double Price { get; set; }

  public ICollection<ProductImage> ProductImages { get; set; } = [];
  public ICollection<Size> Sizes { get; set; } = [];
  public ICollection<Review> Reviews { get; set; } = [];

  public DateTime Created { get; set; }
  public DateTime Updated { get; set; }
}