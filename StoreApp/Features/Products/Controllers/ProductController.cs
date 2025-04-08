using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Features.Products.DTOs;
using StoreApp.Features.Products.Filters;

namespace StoreApp.Features.Products.Controllers;

[ApiController, Route("api/v1/products")]
public class ProductController(StoreDbContext context) : ControllerBase
{
  [HttpGet("list"), Authorize]
  public async Task<ActionResult<IEnumerable<ProductListDto>>> ListProducts([FromQuery] ProductFilters filters)
  {
    int.TryParse(User.FindFirstValue("userid"), out var userId);

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
        IsLiked = false,
        Price = p.Price,
        Discount = 0
      }
    ).ToListAsync();
  }
}