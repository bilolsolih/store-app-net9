using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Exceptions;
using StoreApp.Features.Authentication.DTOs;
using StoreApp.Features.Authentication.Models;

namespace StoreApp.Features.Authentication.Controllers;

[ApiController, Route("api/v1/devices"), Authorize]
public class DeviceController(StoreDbContext context) : ControllerBase
{
  [HttpPost("create")]
  public async Task<ActionResult<DeviceCreateDto>> CreateDevice(DeviceCreateDto payload)
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await context.Users.FindAsync(userId);
    DoesNotExistException.ThrowIfNull(user, $"userId: {userId}");

    var alreadyExists = await context.Devices
      .AnyAsync(d => d.UserId == user.Id && d.FcmToken == payload.FcmToken);

    if (!alreadyExists)
    {
      var newDevice = new Device
      {
        UserId = user.Id,
        FcmToken = payload.FcmToken
      };

      context.Devices.Add(newDevice);
      await context.SaveChangesAsync();
    }


    return Ok(payload);
  }
}