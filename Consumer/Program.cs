using System.Reflection;
using System.Text.Json;
using Consumer;
using Events;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using EventHandler = Consumer.EventHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<UserStore>();
builder.Services.AddScoped<EventHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference().WithName("Consumer API");//available at {base}/scalar
}

app.UseHttpsRedirection();

app.MapPost("/event", ([FromServices]EventHandler eventHandler, [FromBody]Event @event) =>
{
    eventHandler.HandleEvent(@event);
});
app.MapGet("/users", (UserStore userStore) => userStore.GetUsers());

app.MapGet("/health", () => "OK").WithName("Health");

app.Run();