namespace StoreApp.Features.Notifications.DTOs;

public record NotificationTypeCreateDto
{
  public required string Title { get; set; }
  public required IFormFile Icon { get; set; }
}

public record NotificationTypeListDto
{
  public required int Id { get; set; }
  public required string Title { get; set; }
  public required string Icon { get; set; }
}