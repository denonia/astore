using Astore.Application;
using Astore.Domain;
using Astore.WebApi.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Astore.WebApi.Users;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = HttpContext.GetUserId();
        
        var userProfile = await _userService.GetUserProfileAsync(userId);
        return Ok(_mapper.Map<GetUserResponse>(userProfile));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        var userProfile = await _userService.GetUserProfileAsync(id);
        if (userProfile == null)
            return NotFound();
        
        return Ok(_mapper.Map<GetUserResponse>(userProfile));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCurrentUser([FromBody] UpdateUserRequest request)
    {
        var userId = HttpContext.GetUserId();
        
        var userProfile = await _userService.GetUserProfileAsync(userId);
        _mapper.Map<UpdateUserRequest, UserProfile>(request, userProfile);
        await _userService.UpdateUserProfileAsync(userProfile);
        return Ok(_mapper.Map<GetUserResponse>(userProfile));
    }
}