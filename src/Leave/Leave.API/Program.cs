using System.Collections.ObjectModel;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Infrastructure.AutoMapper.Extensions.Autofac;
using Infrastructure.MediatR.Extensions.Autofac;
using Leave.Application.Commands.EmployeeLeaveCommands;
using Leave.Application.Mappings;
using Leave.Application.Queries.EmployeeLeaveQueries;
using Leave.Infrastructure;
using Leave.Infrastructure.Behavior;
using MediatR;
using Shared.Common.Extensions.Configuration;
using Shared.Infrastructure.Autofac;

var builder = WebApplication.CreateBuilder(args);

// Call UseServiceProviderFactory on the Host sub property 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Call ConfigureContainer on the Host sub property 
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.AddAutofac(ConfigurationHelper.DbParams);
    builder.AddLeaveDbContext();
    builder.AddMediatR(typeof(CreateEmployeeLeaveCommandHandler).GetTypeInfo().Assembly);
    builder.RegisterGeneric(typeof(TransactionBehavior<,>)).As(typeof(IPipelineBehavior<,>));
    builder.AddAutoMapper(new Collection<Profile> { new ToDomainProfile(), new ToDtosProfile() });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/EmployeeLeave", async (IMediator mediator) => await mediator.Send(new GetAllQuery()));
app.MapGet("/EmployeeLeave/{id:guid}", async (IMediator mediator, Guid id) => await mediator.Send(new GetQuery(id)));
app.MapPost("/EmployeeLeave", async (IMediator mediator, CreateEmployeeLeaveCommand request) => await mediator.Send(request));

app.Run();