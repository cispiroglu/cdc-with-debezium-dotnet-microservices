using System.Collections.ObjectModel;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Infrastructure.AutoMapper.Extensions.Autofac;
using Infrastructure.Kafka.Extensions.Autofac;
using Infrastructure.MediatR.Extensions.Autofac;
using MediatR;
using Shared.API;
using Shared.Common.Extensions.Configuration;
using Shared.Infrastructure.Autofac;
using Shift.Application.Events;
using Shift.Application.Mappings;
using Shift.Application.Queries.TimeOffQueries;
using Shift.Infrastructure;
using Shift.Infrastructure.Behavior;
using Shift.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Call UseServiceProviderFactory on the Host sub property 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Call ConfigureContainer on the Host sub property 
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.AddAutofac(ConfigurationHelper.DbParams);
    builder.AddShiftDbContext();
    builder.AddMediatR(typeof(TimeOffQueryHandler).GetTypeInfo().Assembly);
    builder.RegisterGeneric(typeof(TransactionBehavior<,>)).As(typeof(IPipelineBehavior<,>));
    builder.AddAutoMapper(new Collection<Profile> { new ToDomainProfile(), new ToDtosProfile() });
    
    builder.AddKafkaConsumer<string, EmployeeLeaveCreatedEvent, EmployeeLeaveCreatedEventHandler, ShiftDbContext>("employee_leave_events", "employee_events_time_off_group", "localhost:9092");
});

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddHostedService<ApplicationLifeCycleService>();

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

app.MapGet("/TimeOff", async (IMediator mediator) => await mediator.Send(new GetAllQuery()));
app.MapGet("/TimeOff/{id:guid}", async (IMediator mediator, Guid id) => await mediator.Send(new GetQuery(id)));

app.Run();