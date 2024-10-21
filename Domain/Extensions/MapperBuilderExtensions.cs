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
        builder.Services.AddAutoMapper(o => o.AddProfiles(GetAutoMapperProfiles()));
    }

    private static IEnumerable<Profile> GetAutoMapperProfiles()
        => Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.Namespace == MappersNamespace)
            .Where(type => type.IsClass)
            .Where(type => type.IsAssignableFrom(typeof(Profile)))
            .Select(type => Activator.CreateInstance(type) as Profile)!;
}