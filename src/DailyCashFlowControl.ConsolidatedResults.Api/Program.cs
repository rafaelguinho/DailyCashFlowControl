using DailyCashFlowControl.ConsolidatedResults.Application.Handlers;
using DailyCashFlowControl.ConsolidatedResults.Application.Queries;
using DailyCashFlowControl.Main;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateConsolidatedItemResultHandler).GetTypeInfo().Assembly));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddConsolidatedResultInfraestructure();
builder.Services.AddRabbitMQ();
builder.Services.AddMessageConsumer();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

app.UseCors("corsapp");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Urls.Add("https://*:7232");
}

app.UseHttpsRedirection();

app.Urls.Add("http://*:5001");


app.MapGet("/consolidatedresults", async ([FromQuery] DateTime? date, IMediator mediator) =>
{
    var result = await mediator.Send(new GetConsolidatedResultQuery { Date = date });

    if(result == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(result);
})
.WithName("ConsolidatedResults")
.WithOpenApi();

app.Run();

