using System.Text.Json.Serialization;

namespace StoreApp.Features.Products.Models;

public class Size
{
  public int Id { get; set; }
  public required string Title { get; set; }
  public string? Description { get; set; }

  [JsonIgnore]
  public ICollection<Product> Products { get; set; } = [];

  public DateTime Created { get; set; }
  public DateTime Updated { get; set; }
}