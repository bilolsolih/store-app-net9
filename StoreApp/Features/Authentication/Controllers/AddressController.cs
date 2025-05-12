using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Exceptions;
using StoreApp.Features.Authentication.DTOs;
using StoreApp.Features.Authentication.Models;

namespace StoreApp.Features.Authentication.Controllers;

[ApiController, Route("api/v1/addresses"), Authorize]
public class AddressController(StoreDbContext context, IMapper mapper) : ControllerBase
{
  [HttpPost("create")]
  public async Task<ActionResult<AddressCreateDto>> CreateNewAddress(AddressCreateDto payload)
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await context.Users.FindAsync(userId);
    DoesNotExistException.ThrowIfNull(user, $"userId: {userId}");

    var newAddress = mapper.Map<Address>(payload);
    newAddress.UserId = user.Id;
    context.Addresses.Add(newAddress);
    await context.SaveChangesAsync();
    return Ok(payload);
  }

  [HttpGet("list")]
  public async Task<ActionResult<IEnumerable<AddressListDto>>> ListAllAddresses()
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await context.Users.FindAsync(userId);
    DoesNotExistException.ThrowIfNull(user, $"userId: {userId}");
    var addresses = await context.Addresses
      .Where(a => a.UserId == user.Id)
      .ProjectTo<AddressListDto>(mapper.ConfigurationProvider)
      .ToListAsync();
    return Ok(addresses);
  }
}