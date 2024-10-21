using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Domain.Extensions;

public static class MapperBuilderExtensions
{
    private const string MappersNamespace = "Domain.Mapper";
    
    public static void AddMappers(this IHostApplicationBuilder builder)
    {
        var mapperProfiles = GetAutoMapperProfiles();
        builder.Services.AddAutoMapper(o => o.AddProfiles(mapperProfiles));
    }

    private static IEnumerable<Profile> GetAutoMapperProfiles()
        => Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.Namespace == MappersNamespace)
            .Where(type => type.IsClass)
            .Where(type => typeof(Profile).IsAssignableFrom(type))
            .Select(type => Activator.CreateInstance(type) as Profile)!;
}