using AutoMapper;
using StoreApp.Features.Notifications.DTOs;
using StoreApp.Features.Notifications.Models;

namespace StoreApp.Features.Notifications.Profiles;

public class NotificationTypeProfile : Profile
{
  public NotificationTypeProfile()
  {
    CreateMap<NotificationType, NotificationTypeListDto>();
  }
  
}