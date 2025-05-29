using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Exceptions;
using StoreApp.Features.Notifications.DTOs;
using StoreApp.Features.Notifications.Models;

namespace StoreApp.Features.Notifications.Controllers;

[ApiController, Route("ap1/v1/notification-type")]
public class NotificationTypeController(StoreDbContext context, IWebHostEnvironment wEnv, IMapper mapper)
  : ControllerBase
{
  [HttpPost("create")]
  public async Task<ActionResult<NotificationTypeCreateDto>> CreateNotificationType(
    [FromForm] NotificationTypeCreateDto payload)
  {
    var alreadyExists = await context.NotificationTypes.AnyAsync(n => n.Title == payload.Title);
    AlreadyExistsException.ThrowIf(alreadyExists, payload.ToString());

    var uploadsDir = wEnv.ContentRootPath + '/' + "uploads/notification_types";
    if (!Directory.Exists(uploadsDir))
    {
      Directory.CreateDirectory(uploadsDir);
    }

    var filePath = uploadsDir + '/' + payload.Icon.FileName;
    await using var newIcon = new FileStream(filePath, FileMode.Create);
    await payload.Icon.CopyToAsync(newIcon);

    var newNotificationType = new NotificationType
    {
      Title = payload.Title,
      Icon = $"notification_types/{payload.Icon.FileName}"
    };

    context.NotificationTypes.Add(newNotificationType);
    await context.SaveChangesAsync();
    return Ok(newNotificationType);
  }

  [HttpGet("list")]
  public async Task<ActionResult<NotificationTypeListDto>> ListAllNotificationTypes()
  {
    var allNotificationTypes = await context.NotificationTypes
      .ProjectTo<NotificationTypeListDto>(mapper.ConfigurationProvider)
      .ToListAsync();

    allNotificationTypes.ForEach(nt => { nt.Icon = $"{HttpContext.GetUploadsBaseUrl()}/{nt.Icon}"; });

    return Ok(allNotificationTypes);
  }
}