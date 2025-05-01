using AutoMapper;
using StoreApp.Features.Notifications.DTOs;
using StoreApp.Features.Notifications.Models;

namespace StoreApp.Features.Notifications.Profiles;

public class NotificationProfiles : Profile
{
  public NotificationProfiles()
  {
    CreateMap<NotificationCreateDto, Notification>();
    CreateMap<Notification, NotificationListDto>()
      .ForMember(dest => dest.Icon, opts => opts.MapFrom(src => src.NotificationType.Icon))
      .ForMember(dest => dest.Content, opts => opts.MapFrom(src => src.Body))
      .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Created))
      ;
  }
}