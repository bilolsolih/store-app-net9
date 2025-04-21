using StoreApp.Features.Notifications.Services;

namespace StoreApp.Features.Notifications;

public static class Extensions
{
  public static void RegisterNotificationsFeature(this IServiceCollection services)
  {
    services.AddSingleton<FirebaseService>();
  }
}