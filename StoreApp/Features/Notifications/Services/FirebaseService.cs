using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using StoreApp.Features.Authentication.Models;
using Notification = StoreApp.Features.Notifications.Models.Notification;

namespace StoreApp.Features.Notifications.Services;

public class FirebaseService
{
  public FirebaseService()
  {
    FirebaseApp.Create(
      new AppOptions()
      {
        Credential = GoogleCredential.FromFile("firebase-key.json")
      }
    );
  }

  public async Task SendNotificationToDevices(List<Device> devices, Notification notification)
  {
    var multicastMessage = new MulticastMessage
    {
      Tokens = devices.Select(device => device.FcmToken).ToList(),
      Notification = new FirebaseAdmin.Messaging.Notification
      {
        Title = notification.Title,
        Body = notification.Body
      },
    };

    await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(multicastMessage);
  }
}