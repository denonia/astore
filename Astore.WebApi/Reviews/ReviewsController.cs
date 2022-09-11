using Astore.Application;
using Astore.Domain;
using Astore.WebApi.Articles;
using Astore.WebApi.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Astore.WebApi.Reviews;

[Route("articles/{articleId}/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IArticleService _articleService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public ReviewsController(IReviewService reviewService, IArticleService articleService, IUserService userService, IMapper mapper)
    {
        _reviewService = reviewService;
        _articleService = articleService;
        _userService = userService;
        _mapper = mapper;
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<IActionResult> Create([FromRoute] Guid articleId, [FromBody] CreateReviewRequest request)
    {
        var article = await _articleService.GetArticleByIdAsync(articleId);
        if (article == null)
            return NotFound("Article doesn't exist");

        var review = _mapper.Map<Review>(request);
        review.Author = await _userService.GetUserProfileAsync(Guid.Parse(HttpContext.GetUserId()));
        review.Article = article;
        await _reviewService.PostReviewAsync(review);
        return CreatedAtAction(nameof(Get), 
            new { articleId = article.Id, id = review.Id }, 
            _mapper.Map<Review, GetReviewResponse>(review));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromRoute] Guid articleId)
    {
        var reviews = await _reviewService.GetAllReviewsAsync(articleId);
        
        return Ok(_mapper.Map<ICollection<GetReviewResponse>>(reviews));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var review = await _reviewService.GetReviewByIdAsync(id);
        if (review == null)
            return NotFound("Review doesn't exist");

        return Ok(_mapper.Map<GetReviewResponse>(review));
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateReviewRequest request)
    {
        var oldReview = await _reviewService.GetReviewByIdAsync(id);
        if (oldReview == null)
            return NotFound("Review doesn't exist");
        if (oldReview.Author.UserId != id)
            return Unauthorized();
        
        var newReview = _mapper.Map(request, oldReview);
        await _reviewService.UpdateReviewAsync(newReview);
        return Ok(_mapper.Map<GetReviewResponse>(newReview));
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var review = await _reviewService.GetReviewByIdAsync(id);
        if (review == null)
            return NotFound("Review doesn't exist");
        if (review.Author.UserId != id)
            return Unauthorized();
        
        await _reviewService.DeleteReviewAsync(review.Id);
        return NoContent();
    }
}