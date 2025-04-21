using StoreApp.Core;
using StoreApp.Features.Authentication.Models;

namespace StoreApp.Features.Notifications.Models;

public class Notification : BaseModel
{
  public NotificationType NotificationType { get; set; }
  public required int NotificationTypeId { get; set; }
  
  public required string Title { get; set; }
  public required string Body { get; set; }
  public ICollection<Device> SentDevices { get; set; } = [];
}