using AutoMapper;
using StoreApp.Features.Notifications.DTOs;
using StoreApp.Features.Notifications.Models;

namespace StoreApp.Features.Notifications.Profiles;

public class NotificationProfiles : Profile
{
  public NotificationProfiles()
  {
    CreateMap<NotificationCreateDto, Notification>();
  }
}