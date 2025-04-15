using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Features.Products.DTOs;

namespace StoreApp.Features.Products.Controllers;

[ApiController, Route("api/v1/categories"), Authorize]
public class CategoryController(StoreDbContext context, IMapper mapper) : ControllerBase
{
  [HttpGet("list")]
  public async Task<ActionResult<CategoryListDto>> ListCategories()
  {
    var categories = await context.Categories
      .ProjectTo<CategoryListDto>(mapper.ConfigurationProvider)
      .ToListAsync();

    return Ok(categories);
  }
}