using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Exceptions;
using StoreApp.Core.Filters;
using StoreApp.Features.Reviews.DTOs;
using StoreApp.Features.Reviews.Models;

namespace StoreApp.Features.Reviews.Controllers;

[ApiController, Route("api/v1/reviews"), Authorize]
public class ReviewController(StoreDbContext context, IMapper mapper) : ControllerBase
{
  [HttpGet("list/{productId:int}")]
  public async Task<ActionResult<IEnumerable<ReviewListDto>>> GetAllReviews(int productId,
    [FromQuery] PaginationFilters filters)
  {
    var product = await context.Products.SingleOrDefaultAsync(p => p.Id == productId);
    DoesNotExistException.ThrowIfNull(product, $"productId: {productId}");

    var query = context.Reviews.Where(r => r.ProductId == product.Id);
    if (filters is { Limit: not null, Page: not null })
    {
      query = query.Skip(filters.Limit.Value * (filters.Page.Value - 1)).Take(filters.Limit.Value);
    }

    var reviews = await query.ProjectTo<ReviewListDto>(mapper.ConfigurationProvider).ToListAsync();

    return Ok(reviews);
  }

  [HttpPost("create")]
  public async Task<ActionResult<ReviewCreateDto>> CreateReview(ReviewCreateDto payload)
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await context.Users.FindAsync(userId);
    DoesNotExistException.ThrowIfNull(user, $"userId: {userId}");

    var product = await context.Products.SingleOrDefaultAsync(p => p.Id == payload.ProductId);
    DoesNotExistException.ThrowIfNull(product, $"productId: {payload.ProductId}");

    if (await context.Reviews.AnyAsync(r => r.ProductId == payload.ProductId && r.UserId == userId))
    {
      throw new AlreadyExistsException("Review by this user for this product already exists!");
    }

    var newReview = mapper.Map<Review>(payload);
    newReview.UserId = user.Id;
    context.Reviews.Add(newReview);
    await context.SaveChangesAsync();
    return payload;
  }

  [HttpGet("stats/{productId:int}")]
  public async Task<ActionResult<ReviewStatsDto>> GetReviewStatsForProduct(int productId)
  {
    var product = await context.Products.FindAsync(productId);
    DoesNotExistException.ThrowIfNull(product, $"productId: {productId}");

    var stats = await context.Reviews
      .Where(r => r.ProductId == product.Id)
      .GroupBy(r => r.Rating)
      .Select(gr => new { Rating = gr.Key, Count = gr.Count() })
      .ToListAsync();

    var totalCounts = stats.Sum(r => r.Count);

    var reviewStats = new ReviewStatsDto
    {
      TotalCount = totalCounts,
      FiveStars = stats.FirstOrDefault(gr => gr.Rating == 5)?.Count ?? 0,
      FourStars = stats.FirstOrDefault(gr => gr.Rating == 4)?.Count ?? 0,
      ThreeStars = stats.FirstOrDefault(gr => gr.Rating == 3)?.Count ?? 0,
      TwoStars = stats.FirstOrDefault(gr => gr.Rating == 2)?.Count ?? 0,
      OneStars = stats.FirstOrDefault(gr => gr.Rating == 1)?.Count ?? 0,
    };

    return Ok(reviewStats);
  }
}