using DailyCashFlowControl.Transactions.Application.Handlers;
using DailyCashFlowControl.Domain.Models.Requests;
using DailyCashFlowControl.Main;
using MediatR;
using FluentValidation;
using System.Reflection;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using DailyCashFlowControl.Transactions.Application.Commands;
using DailyCashFlowControl.Transactions.Application.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddTransactionHandler).GetTypeInfo().Assembly));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRabbitMQ();
builder.Services.AddMessageProducer();
builder.Services.AddTransactionInfraestructure();

builder.Services.AddValidatorsFromAssemblyContaining<TransactionRequestValidator>();

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
    app.Urls.Add("https://*:7231");
}

app.UseHttpsRedirection();

app.Urls.Add("http://*:5000");


app.MapPost("/transactions", async (TransactionRequest transaction, IValidator<TransactionRequest> validator, IMediator mediator) =>
{
    var validationResult = validator.Validate(transaction);

    if (validationResult.IsValid)
    {
        await mediator.Send(new TransactionCommand(transaction.Type, transaction.Value, transaction.Description, transaction.HubClientId));
        // do the thing
        return Results.Ok();
    }

    return Results.ValidationProblem(validationResult.ToDictionary(),
        statusCode: (int)HttpStatusCode.UnprocessableEntity);
});

app.MapGet("/transactions", async ([FromQuery] string? search, IMediator mediator) =>
{
    var result = await mediator.Send(new SearchTransactionsQuery { Description = search });

    if (result == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(result);
});

app.Run();

