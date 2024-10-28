using System.Runtime.CompilerServices;
using Api.Exceptions.Handlers;
using Api.Services.Abstraction;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Extensions;

public static class ApiBuilderExtensions
{
    private const string ServicesNameSpace = "Api.Services";
    
    public static void AddServices(this IHostApplicationBuilder builder)
    {
        typeof(IService).Assembly
            .GetTypes()
            .Where(type => type.Namespace == ServicesNameSpace)
            .Where(type => type.GetInterface(nameof(IService)) is not null)
            .Where(type => type is { IsClass: true, IsAbstract: false, IsInterface: false })
            .ToList()
            .ForEach(type => builder.Services.AddTransient(type.GetInterface($"I{type.Name}")!, type));
    }

    public static void AddExceptionHandlers(this IHostApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
        builder.Services.AddExceptionHandler<ConflictExceptionHandler>();
        builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();

        builder.Services.AddProblemDetails();
    }
}