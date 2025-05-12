using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Exceptions;
using StoreApp.Features.Cart.DTOs;
using StoreApp.Features.Cart.Models;

namespace StoreApp.Features.Cart.Controllers;

[ApiController, Route("api/v1/my-cart"), Authorize]
public class CartItemController(StoreDbContext context, IMapper mapper) : ControllerBase
{
  [HttpPost("add-item")]
  public async Task<ActionResult<CartItem>> CreateCartItem(CartItemCreateDto payload)
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await context.Users
      .SingleOrDefaultAsync(u => u.Id == userId);
    DoesNotExistException.ThrowIfNull(user, $"userId: {userId}");

    var cart = await context.UserCarts.SingleOrDefaultAsync(c => c.UserId == user.Id);
    if (cart == null)
    {
      var newCart = new UserCart
      {
        UserId = user.Id
      };
      context.UserCarts.Add(newCart);
      await context.SaveChangesAsync();
      cart = newCart;
    }

    var product = await context.Products
      .Include(p => p.Sizes)
      .SingleOrDefaultAsync(p => p.Id == payload.ProductId);
    DoesNotExistException.ThrowIfNull(product, $"productId: {payload.ProductId}");

    var size = await context.Sizes.FindAsync(payload.SizeId);
    DoesNotExistException.ThrowIfNull(size, $"sizeId: {payload.SizeId}");

    if (product.Sizes.All(s => s.Id != size.Id))
    {
      throw new DoesNotExistException($"There is no such Size available for the productId: {product.Id}");
    }

    var cartItem = await context.CartItems
      .Include(c => c.UserCart)
      .SingleOrDefaultAsync(c => c.UserCart.UserId == user.Id && c.SizeId == size.Id && c.ProductId == product.Id);

    if (cartItem == null)
    {
      var newCartItem = new CartItem
      {
        UserCartId = cart.Id,
        ProductId = product.Id,
        SizeId = size.Id,
        Quantity = 1
      };

      context.CartItems.Add(newCartItem);
    }
    else
    {
      cartItem.Quantity += 1;
    }

    await context.SaveChangesAsync();
    return Ok();
  }

  [HttpGet("my-cart-items")]
  public async Task<ActionResult<MyCartDto>> ListMyCartItems()
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await context.Users
      .SingleOrDefaultAsync(u => u.Id == userId);
    DoesNotExistException.ThrowIfNull(user, $"userId: {userId}");

    var cartItems = await context.CartItems
      .Include(c => c.Product).ThenInclude(p => p.ProductImages)
      .Include(c => c.UserCart)
      .Where(c => c.UserCart.UserId == user.Id)
      .ToListAsync();

    var mappedCartItems = mapper.Map<List<CartItemListDto>>(cartItems);
    mappedCartItems.ForEach(item=>item.Image = $"{HttpContext.GetUploadsBaseUrl()}/{item.Image}");

    var subTotal = cartItems.Sum(c => c.Product.Price * c.Quantity);

    var myCart = new MyCartDto
    {
      Items = mappedCartItems,
      ShippingFee = 80,
      SubTotal = subTotal,
      VAT = 0,
      Total = subTotal + 80,
    };

    return Ok(myCart);
  }
}