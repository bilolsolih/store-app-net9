using StoreApp.Core;

namespace StoreApp.Features.Notifications.Models;

public class NotificationType : BaseModel
{
  public required string Title { get; set; }
  public required string Icon { get; set; }
}