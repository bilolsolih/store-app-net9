namespace StoreApp.Features.Reviews.DTOs;

public record ReviewListDto
{
  public required int Id { get; set; }
  public required string Comment { get; set; }
  public required int Rating { get; set; }
  public required DateTime Created { get; set; }
  public required string UserFullName { get; set; }
}