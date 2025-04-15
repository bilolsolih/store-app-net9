using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Features.Products.DTOs;

namespace StoreApp.Features.Products.Controllers;

[ApiController, Route("api/v1/sizes"), Authorize]
public class SizeController(StoreDbContext context, IMapper mapper) : ControllerBase
{
  [HttpGet("list")]
  public async Task<ActionResult<CategoryListDto>> ListSizes()
  {
    var sizes = await context.Sizes
      .ProjectTo<SizeListDto>(mapper.ConfigurationProvider)
      .ToListAsync();

    return Ok(sizes);
  }
}