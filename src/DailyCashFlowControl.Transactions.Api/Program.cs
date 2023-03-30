using DailyCashFlowControl.Application.Handlers;
using DailyCashFlowControl.Domain.Interfaces;
using DailyCashFlowControl.Domain.Models.Requests;
using DailyCashFlowControl.Main;
using MediatR;
using FluentValidation;
using System.Reflection;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using System;
using System.ComponentModel.DataAnnotations;
using DailyCashFlowControl.Application.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddTransactionHandler).GetTypeInfo().Assembly));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRabbitMQ();
builder.Services.AddMessageProducer();
builder.Services.AddTransactionInfraestructure();
builder.Services.AddValidatorsFromAssemblyContaining<TransactionRequestValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Urls.Add("http://*:5000");
//app.Urls.Add("https://*:7231");


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


app.MapPost("/transaction", async (TransactionRequest transaction, IValidator<TransactionRequest> validator, IMediator mediator) =>
{
    //_messagePublisher.SendMessage(transaction);

    var validationResult = validator.Validate(transaction);

    if (validationResult.IsValid)
    {
        await mediator.Send(new TransactionCommand(transaction.Type, transaction.Value));
        // do the thing
        return Results.Ok();
    }

    return Results.ValidationProblem(validationResult.ToDictionary(),
        statusCode: (int)HttpStatusCode.UnprocessableEntity);
});

//app.MapPost("/person", (Validated<Person> req) =>
//{
//    // deconstruct to bool & Person
//    var (isValid, value) = req;

//    return isValid
//        ? Results.Ok(value)
//        : Results.ValidationProblem(req.Errors);
//});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public record Person(string? Name, int? Age);

// ReSharper disable once UnusedType.Global
public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(m => m.Name).NotEmpty();
        RuleFor(m => m.Age).NotEmpty().GreaterThan(0);
    }
}


