using Api.Exceptions;
using Api.Services.Abstraction;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Security.Enums;

namespace Api.MinimalApi;

public static class UserEndpoints
{
    public static WebApplication Register(this WebApplication app)
    {
        app.MapPost("/user/register", async ([FromBody] RegisterUser user, [FromServices] IUserService userService) =>
        {
            if (!Role.IsValidRole(user.Role))
            {
                throw new BadRequestException($"Role {user.Role} does not exist.");
            }

            var result = await userService.Register(user);
            return result ? Results.Created() : Results.Problem();
        }).WithTags("user");
        return app;
    }

    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        return app.Register();
    }
}