namespace StoreApp.Features.Products.Models;

public class Category
{
  public int Id { get; set; }
  public required string Title { get; set; }

  public ICollection<Product> Products { get; set; } = [];

  public DateTime Created { get; set; }
  public DateTime Updated { get; set; }
}