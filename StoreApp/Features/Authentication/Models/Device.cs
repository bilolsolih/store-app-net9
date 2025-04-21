using StoreApp.Core;
using StoreApp.Features.Notifications.Models;

namespace StoreApp.Features.Authentication.Models;

public class Device : BaseModel
{
  public required string FcmToken { get; set; }

  public User User { get; set; }
  public required int UserId { get; set; }

  public ICollection<Notification> ReceivedNotifications { get; set; } = [];
}