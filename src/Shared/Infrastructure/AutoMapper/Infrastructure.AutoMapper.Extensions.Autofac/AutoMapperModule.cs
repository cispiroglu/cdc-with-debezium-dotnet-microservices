using Autofac;
using AutoMapper;
using Shared.Common;

namespace Infrastructure.AutoMapper.Extensions.Autofac;

public class AutoMapperModule : Module
{
    private readonly IReadOnlyList<Profile> _profiles;

    public AutoMapperModule(IReadOnlyList<Profile> profiles)
    {
        _profiles = profiles;
    }

    protected override void Load(ContainerBuilder builder)
    {
        var mapper = new MapperConfiguration(c => c.AddProfiles(_profiles)).CreateMapper();
        builder.RegisterInstance(mapper).As<IMapper>().SingleInstance();
            
        builder.RegisterType<ApplicationMapper>()
            .As<IApplicationMapper>()
            .InstancePerLifetimeScope();
    }
}