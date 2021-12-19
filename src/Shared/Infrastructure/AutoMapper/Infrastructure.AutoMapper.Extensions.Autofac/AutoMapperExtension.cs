using Autofac;
using AutoMapper;

namespace Infrastructure.AutoMapper.Extensions.Autofac;

public static class AutoMapperExtension
{
    public static void AddAutoMapper(this ContainerBuilder builder, IReadOnlyList<Profile> profiles)
        => builder.RegisterModule(new AutoMapperModule(profiles));
}