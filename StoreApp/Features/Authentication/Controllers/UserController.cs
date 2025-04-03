using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Features.Authentication.DTOs;
using StoreApp.Features.Authentication.Models;
using StoreApp.Features.Authentication.Services;

namespace StoreApp.Features.Authentication.Controllers;

[ApiController, Route("api/v1/auth")]
public class UserController(UserService service, TokenService tokenService) : ControllerBase
{
  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginDto payload)
  {
    var user = await service.GetUserByLoginAsync(payload.Login);
    if (user == null)
    {
      return Unauthorized();
    }

    if (payload.Password == user.Password)
    {
      var token = await tokenService.GenerateTokenAsync(user.Email, user.Id);
      return Ok(new { AccessToken = token });
    }

    return Unauthorized();
  }

  [HttpGet("me"), Authorize]
  public async Task<ActionResult<UserDetailDto>> GetMyDetails()
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await service.GetUserByIdAsync(userId);
    return Ok(user);
  }

  [HttpPost("register")]
  public async Task<ActionResult<Dictionary<string, string>>> Register(UserCreateDto payload)
  {
    var user = await service.CreateUserAsync(payload);
    var token = await tokenService.GenerateTokenAsync(user.Email, user.Id);
    return StatusCode(201, new { AccessToken = token });
  }

  [HttpGet("details/{id:int}")]
  public async Task<ActionResult<UserDetailDto>> GetUser(int id)
  {
    var user = await service.GetUserByIdAsync(id);
    return Ok(user);
  }

  [HttpPatch("update"), Authorize]
  public async Task<ActionResult<UserDetailDto>> Update(UserUpdateDto payload)
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await service.UpdateUserAsync(userId, payload);
    return Ok(user);
  }
}