using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Exceptions;
using StoreApp.Features.Orders.DTOs;
using StoreApp.Features.Orders.Models;

namespace StoreApp.Features.Orders.Controllers;

[ApiController, Route("api/v1/orders"), Authorize]
public class OrderController(StoreDbContext context) : ControllerBase
{
  [HttpPost("create")]
  public async Task<ActionResult> CreateOrder(OrderCreateDto payload)
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await context.Users.FindAsync(userId);
    DoesNotExistException.ThrowIfNull(user, $"userId: {userId}");

    var userCart = await context.UserCarts.SingleOrDefaultAsync(c => c.UserId == user.Id);
    DoesNotExistException.ThrowIfNull(userCart, "User does not have a cart.");

    var userCartItems = await context.CartItems
      .Where(c => c.UserCartId == userCart.Id)
      .Include(cartItem => cartItem.Product)
      .ToListAsync();

    if (userCartItems.Count <= 0)
    {
      return BadRequest("User does not have any items in his cart.");
    }

    var subTotal = userCartItems.Sum(c => c.Quantity * c.Product.Price);

    var newOrder = new Order
    {
      UserId = user.Id,
      AddressId = payload.AddressId,
      PaymentMethod = payload.PaymentMethod,
      CardId = payload.CardId,
      ShippingFee = 80,
      VAT = 0,
      SubTotal = userCartItems.Sum(c => c.Quantity * c.Product.Price),
      Total = subTotal + 80,
      Status = OrderStatus.Packing,
    };

    await using var transaction = await context.Database.BeginTransactionAsync();

    try
    {
      context.Orders.Add(newOrder);
      await context.SaveChangesAsync();

      foreach (var item in userCartItems)
      {
        var newOrderItem = new OrderItem()
        {
          OrderId = newOrder.Id,
          Quantity = item.Quantity,
          ProductId = item.ProductId,
          SizeId = item.SizeId,
          PriceTotal = item.Quantity * item.Product.Price,
        };

        context.OrderItems.Add(newOrderItem);
        context.CartItems.Remove(item);
      }

      await context.SaveChangesAsync();
      await transaction.CommitAsync();
    }
    catch (Exception ex)
    {
      await transaction.RollbackAsync();
      throw;
    }

    return Ok();
  }

  [HttpGet("list")]
  public async Task<ActionResult> ListOrders()
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await context.Users.FindAsync(userId);
    DoesNotExistException.ThrowIfNull(user, $"userId: {userId}");

    var userOrders = await context.Orders
      .Where(o => o.UserId == user.Id)
      .Include(o => o.OrderItems)
      .ThenInclude(orderItem => orderItem.Product)
      .ThenInclude(product => product.ProductImages)
      .Include(order => order.OrderItems)
      .ThenInclude(orderItem => orderItem.Size)
      .ToListAsync();

    List<OrderListDto> orders = [];

    var baseUrl = HttpContext.GetUploadsBaseUrl();
    foreach (var order in userOrders)
    {
      foreach (var orderOrderItem in order.OrderItems)
      {
        orders.Add(
          new OrderListDto
          {
            Id = orderOrderItem.Id,
            Title = orderOrderItem.Product.Title,
            Image = $"{baseUrl}/{orderOrderItem.Product.ProductImages.FirstOrDefault()?.Image}",
            Size = orderOrderItem.Size.Title,
            Price = orderOrderItem.PriceTotal,
            Status = order.Status,
          }
        );
      }
    }

    return Ok(orders);
  }

  [HttpDelete("delete/{id:int}")]
  public async Task<ActionResult> DeleteOrder(int id)
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await context.Users.FindAsync(userId);
    DoesNotExistException.ThrowIfNull(user, $"userId: {userId}");

    var order = await context.Orders.FindAsync(id);
    DoesNotExistException.ThrowIfNull(order, "Such order does not exist.");

    if (order.UserId != user.Id)
    {
      return Forbid("This order does not belong to the user.");
    }

    context.Orders.Remove(order);
    await context.SaveChangesAsync();

    return NoContent();
  }
}