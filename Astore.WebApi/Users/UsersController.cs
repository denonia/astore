﻿using Astore.Application;
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
        if (userId == null)
            return Unauthorized();
        
        var userProfile = await _userService.GetUserProfileAsync(Guid.Parse(userId));
        return Ok(_mapper.Map<GetUserResponse>(userProfile));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCurrentUser([FromBody] UpdateUserRequest request)
    {
        var userId = HttpContext.GetUserId();
        if (userId == null)
            return Unauthorized();
        
        var userProfile = await _userService.GetUserProfileAsync(Guid.Parse(userId));
        _mapper.Map<UpdateUserRequest, UserProfile>(request, userProfile);
        await _userService.UpdateUserProfileAsync(userProfile);
        return Ok(_mapper.Map<GetUserResponse>(userProfile));
    }
}