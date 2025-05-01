namespace StoreApp.Features.Notifications.DTOs;

public record NotificationCreateDto
{
  public required int NotificationTypeId { get; set; }
  public required string Title { get; set; }
  public required string Body { get; set; }
}

public record NotificationListDto
{
  public required int Id { get; set; }
  public required string Title { get; set; }
  public required string Icon { get; set; }
  public required string Content { get; set; }
  public required DateTime Date { get; set; }
}