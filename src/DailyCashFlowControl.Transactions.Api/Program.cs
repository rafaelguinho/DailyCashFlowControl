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
using DailyCashFlowControl.Application.Queries;

var builder = WebApplication.CreateBuilder(args);

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


app.MapPost("/transaction", async (TransactionRequest transaction, IValidator<TransactionRequest> validator, IMediator mediator) =>
{
    var validationResult = validator.Validate(transaction);

    if (validationResult.IsValid)
    {
        await mediator.Send(new TransactionCommand(transaction.Type, transaction.Value, transaction.Description));
        // do the thing
        return Results.Ok();
    }

    return Results.ValidationProblem(validationResult.ToDictionary(),
        statusCode: (int)HttpStatusCode.UnprocessableEntity);
});

app.MapGet("/transaction/search", async ([FromQuery] SearchTransactionsQuery search, IMediator mediator) =>
{
    await mediator.Send(search);
});

app.Run();

