using Astore.Application;
using Astore.WebApi.Articles;
using Astore.WebApi.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Astore.WebApi.Favorites;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("/favorites")]
public class FavoritesController : ControllerBase
{
    private readonly IFavoritesService _favoritesService;
    private readonly IMapper _mapper;

    public FavoritesController(IFavoritesService favoritesService, IMapper mapper)
    {
        _favoritesService = favoritesService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var favorites = await _favoritesService.GetUserFavoritesAsync(HttpContext.GetUserId());
        return Ok(_mapper.Map<ICollection<GetArticleResponse>>(favorites));
    }

    [HttpPost]
    public async Task<IActionResult> MoveToCart()
    {
        var userId = HttpContext.GetUserId();
        await _favoritesService.MoveFavoritesToCartAsync(userId);
        var favorites = _favoritesService.GetUserFavoritesAsync(userId);
        return Ok(_mapper.Map<ICollection<GetArticleResponse>>(favorites));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ICollection<Guid> articleIds)
    {
        var userId = HttpContext.GetUserId();
        await _favoritesService.UpdateFavoritesAsync(userId, articleIds);
        var favorites = _favoritesService.GetUserFavoritesAsync(userId);
        return Ok(_mapper.Map<ICollection<GetArticleResponse>>(favorites));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAll()
    {
        await _favoritesService.ClearFavoritesAsync(HttpContext.GetUserId());
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _favoritesService.DeleteArticleFromFavoritesAsync(HttpContext.GetUserId(), id);
        return NoContent();
    }
}