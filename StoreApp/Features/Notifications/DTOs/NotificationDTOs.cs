namespace StoreApp.Features.Notifications.DTOs;

public record NotificationCreateDto
{
  public required int NotificationTypeId { get; set; }
  public required string Title { get; set; }
  public required string Body { get; set; }
}