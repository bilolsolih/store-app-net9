using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Exceptions;
using StoreApp.Features.Products.DTOs;
using StoreApp.Features.Products.Filters;

namespace StoreApp.Features.Products.Controllers;

[ApiController, Route("api/v1/products")]
public class ProductController(StoreDbContext context, IMapper mapper) : ControllerBase
{
  [HttpGet("list"), Authorize]
  public async Task<ActionResult<IEnumerable<ProductListDto>>> ListProducts([FromQuery] ProductFilters filters)
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await context.Users.Include(u => u.SavedProducts).SingleOrDefaultAsync(u => u.Id == userId);
    DoesNotExistException.ThrowIfNull(user, $"userId: {userId}");

    var products = context.Products
      .Include(p => p.ProductImages).AsQueryable();

    if (filters is { CategoryId: not null })
    {
      products = products.Where(p => p.CategoryId == filters.CategoryId);
    }

    if (filters is { Title: not null })
    {
      products = products.Where(p => p.Title.ToLower().Contains(filters.Title.ToLower()));
    }

    return await products.Select(
      p => new ProductListDto
      {
        Id = p.Id,
        Title = p.Title,
        Image = HttpContext.GetUploadsBaseUrl() + '/' + p.ProductImages.Single(pi => pi.IsMain).Image,
        IsLiked = user.SavedProducts.Contains(p),
        Price = p.Price,
        Discount = 0
      }
    ).ToListAsync();
  }


  [HttpGet("saved-products"), Authorize]
  public async Task<ActionResult<IEnumerable<ProductListDto>>> GetSavedProducts()
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await context.Users
      .Include(u => u.SavedProducts)
      .ThenInclude(sp => sp.ProductImages)
      .SingleOrDefaultAsync(u => u.Id == userId);
    DoesNotExistException.ThrowIfNull(user, $"user_id: {userId}");
    var products = mapper.Map<List<ProductListDto>>(user.SavedProducts);

    for (var i = 0; i < products.Count; i++)
    {
      if (products[i].Image != null)
      {
        products[i].Image = HttpContext.GetUploadsBaseUrl() + '/' + products[i].Image;
      }

      products[i].IsLiked = true;
    }


    return Ok(products);
  }
}