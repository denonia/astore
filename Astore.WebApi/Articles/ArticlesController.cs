using Astore.Application;
using Astore.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Astore.WebApi.Articles;

[Route("/articles")]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _articleService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public ArticlesController(IArticleService articleService, ICategoryService categoryService, IMapper mapper)
    {
        _articleService = articleService;
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var articles = await _articleService.GetArticlesAsync();

        return Ok(_mapper.Map<IEnumerable<GetAllArticlesResponse>>(articles));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var article = await _articleService.GetArticleByIdAsync(id);
        if (article == null)
            return NotFound();

        return Ok(_mapper.Map<GetArticleResponse>(article));
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateArticleRequest request)
    {
        var article = _mapper.Map<Article>(request);
        var category = await _categoryService.GetCategoryByNameAsync(request.Category);

        article.Category = category;
        await _articleService.CreateArticleAsync(article);
        return CreatedAtAction(nameof(Get), new { id = article.Id }, _mapper.Map<GetArticleResponse>(article));
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateArticleRequest request)
    {
        var article = await _articleService.GetArticleByIdAsync(id);
        if (article == null)
            return NotFound();

        var category = await _categoryService.GetCategoryByNameAsync(request.Category);
        article.Category = category;
        _mapper.Map(request, article);
        await _articleService.UpdateArticleAsync(article);
        return Ok(_mapper.Map<GetArticleResponse>(article));
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _articleService.DeleteArticleAsync(id);

        return deleted ? NoContent() : NotFound();
    }
}