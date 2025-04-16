namespace StoreApp.Features.Reviews.DTOs;

public record ReviewStatsDto
{
  public int TotalCount { get; set; }
  public int FiveStars { get; set; }
  public int FourStars { get; set; }
  public int ThreeStars { get; set; }
  public int TwoStars { get; set; }
  public int OneStars { get; set; }
}

public record ReviewListDto
{
  public required int Id { get; set; }
  public required string Comment { get; set; }
  public required int Rating { get; set; }
  public required DateTime Created { get; set; }
  public required string UserFullName { get; set; }
}

public record ReviewCreateDto
{
  public required int ProductId { get; set; }
  public required int Rating { get; set; }
  public string? Comment { get; set; }
}