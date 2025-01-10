using Api.Exceptions;
using Api.Services.Abstraction;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.MinimalApi;

public static class RatingEndpoints
{
    public static WebApplication GetRating(this WebApplication app)
    {
        app.MapGet("/rating/{id:guid}", async ([FromRoute] Guid id, [FromServices] IRatingService ratingService) =>
        {
            var rating = await ratingService.GetRatingAsync(id);
            return rating is not null ? Results.Ok(rating) : Results.NotFound();
        }).RequireAuthorization()
        .WithTags("rating");

        return app;
    }
    
    public static WebApplication CreateRating(this WebApplication app)
    {
        app.MapPost("/rating", async ([FromBody] CreateRatingRequest request, [FromServices] IRatingService ratingService) =>
        {
            var ratingId = await ratingService.CreateRatingAsync(request);
            return Results.CreatedAtRoute($"/rating/{ratingId}");
        }).RequireAuthorization()
        .WithTags("rating");

        return app;
    }
    
    public static WebApplication UpdateRating(this WebApplication app)
    {
        app.MapPut("/rating/{id:guid}", 
            async ([FromRoute] Guid id, [FromBody] UpdateRatingRequest request, [FromServices] IRatingService ratingService) =>
            {
                if (id.Equals(request.Id))
                {
                    throw new ConflictException("Rating ids in path and body do not match. Cannot perform update.");
                }
                
                var result = await ratingService.UpdateRatingAsync(request);

                return Results.Ok(result);
            }).RequireAuthorization()
            .WithTags("rating");

        return app;
    }
    
    public static WebApplication DeleteRating(this WebApplication app)
    {
        app.MapDelete("/rating/{id:guid}", 
            async ([FromRoute] Guid id, [FromBody] DeleteRatingRequest request, [FromServices] IRatingService ratingService) =>
            {
                if (id.Equals(request.Id))
                {
                    throw new ConflictException("Rating ids in path and body do not match. Cannot perform delete.");
                }
                
                await ratingService.DeleteRatingAsync(request.Id);
                return Results.NoContent();
            }).RequireAuthorization()
            .WithTags("rating");

        return app;
    }
    
    public static WebApplication GetUserRating(this WebApplication app)
    {
        app.MapGet("/rating/user", async ([FromBody] UserRatingRequest request, [FromServices] IRatingService ratingService) =>
        {
            var userRating = await ratingService.GetUserRatingAsync(request);
            return userRating is not null ? Results.Ok(userRating) : Results.NotFound();
        }).RequireAuthorization()
        .WithTags("rating");

        return app;
    }
    
    public static WebApplication GetAverageRating(this WebApplication app)
    {
        app.MapGet("/rating/average",
            async ([FromBody] AverageRatingRequest request, [FromServices] IRatingService ratingService) =>
            {
                var averageRating = await ratingService.GetAverageRatingAsync(request.FilmId, request.ShowId, request.EpisodeId);
                return Results.Ok(averageRating);
            }).WithTags("rating");

        return app;
    }

    public static WebApplication MapRatingEndpoints(this WebApplication app)
    {
        return app.GetRating()
            .CreateRating()
            .UpdateRating()
            .DeleteRating()
            .GetUserRating()
            .GetAverageRating();
    }
}