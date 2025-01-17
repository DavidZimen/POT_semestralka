using Api.Exceptions;
using Api.Services;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Security.Enums;

namespace Api.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUser user)
    {
        if (!Role.IsValidRole(user.Role))
        {
            throw new BadRequestException($"Role {user.Role} does not exist.");
        }

        var result = await _userService.Register(user);
        return result ? Created() : Problem();
    }
}