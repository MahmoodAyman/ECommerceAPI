using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CartController(ICartService cartService) : BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<ShoppingCart>> GetCartById(string id)
    {
        var cart = await cartService.GetCartAsync(id);

        return Ok(cart ?? new ShoppingCart { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
    {
        var updateCart = await cartService.SetCartAsync(cart);
        if (updateCart == null) return BadRequest("Something wrong in Cart");
        return updateCart;
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteCart(string id)
    {
        var result = await cartService.DeleteCartAsync(id);
        if (!result) return BadRequest("Somethine went wrong while Deleting Cart, Try again later");
        return Ok();
    }
}

