using Astore.Application;
using Astore.Domain;
using Astore.WebApi.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Astore.WebApi.Cart;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("/cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;

    public CartController(ICartService cartService, IMapper mapper)
    {
        _cartService = cartService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var cartItems = await _cartService.GetCartItemsAsync(HttpContext.GetUserId());
        return Ok(_mapper.Map<ICollection<GetCartItemResponse>>(cartItems));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ICollection<UpdateCartRequest> request)
    {
        var cartItems = _mapper.Map<ICollection<CartItem>>(request);
        await _cartService.UpdateCartAsync(HttpContext.GetUserId(), cartItems);
        return Ok(cartItems);
    }

    [HttpDelete]
    public async Task<IActionResult> Clear()
    {
        await _cartService.ClearCartAsync(HttpContext.GetUserId());
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _cartService.DeleteArticleFromCartAsync(HttpContext.GetUserId(), id);
        return NoContent();
    }
}