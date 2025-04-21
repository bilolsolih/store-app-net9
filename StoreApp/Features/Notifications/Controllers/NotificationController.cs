using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Exceptions;
using StoreApp.Features.Notifications.DTOs;
using StoreApp.Features.Notifications.Models;
using StoreApp.Features.Notifications.Services;

namespace StoreApp.Features.Notifications.Controllers;

[ApiController, Route("api/v1/notifications"), Authorize]
public class NotificationController(StoreDbContext context, IMapper mapper, FirebaseService firebaseService)
  : ControllerBase
{
  [HttpPost("send")]
  public async Task<ActionResult> SendNotification(NotificationCreateDto payload)
  {
    var notificationType = await context.NotificationTypes.FindAsync(payload.NotificationTypeId);
    DoesNotExistException.ThrowIfNull(notificationType, $"notificationTypeId: {payload.NotificationTypeId}");

    var newNotification = mapper.Map<Notification>(payload);
    context.Notifications.Add(newNotification);
    await context.SaveChangesAsync();

    var devices = await context.Devices.ToListAsync();

    await firebaseService.SendNotificationToDevices(devices: devices, notification: newNotification);
    return Ok();
  }
}