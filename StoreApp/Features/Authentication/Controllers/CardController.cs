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

[ApiController, Route("api/v1/cards"), Authorize]
public class CardController(StoreDbContext context, IMapper mapper) : ControllerBase
{
  [HttpPost("create")]
  public async Task<ActionResult> CreateCard(CardCreateDto payload)
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await context.Users.FindAsync(userId);
    DoesNotExistException.ThrowIfNull(user, "Bunday foydalanuvchi mavjud emas.");

    var alreadyExists = await context.Cards.AnyAsync(card => card.CardNumber == payload.CardNumber && card.UserId == user.Id);
    AlreadyExistsException.ThrowIf(alreadyExists, "Foydalanuvchida bunday karta allaqachon mavjud.");

    var newCard = new Card
    {
      UserId = user.Id,
      CardNumber = payload.CardNumber,
      ExpiryDate = payload.ExpiryDate,
      SecurityCode = payload.SecurityCode
    };

    context.Cards.Add(newCard);
    await context.SaveChangesAsync();
    return Ok();
  }

  [HttpGet("list")]
  public async Task<ActionResult<IEnumerable<CardListDto>>> ListCards()
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await context.Users.FindAsync(userId);
    DoesNotExistException.ThrowIfNull(user, "Bunday foydalanuvchi mavjud emas.");

    var cards = await context.Cards
      .Where(c => c.UserId == user.Id)
      .ProjectTo<CardListDto>(mapper.ConfigurationProvider)
      .ToListAsync();
    
    return Ok(cards);
  }
}