using Events;
using Microsoft.AspNetCore.Mvc;
using Provider;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<UserStore>();
builder.Services.AddSingleton<SubscriberStore>();
builder.Services.AddHttpClient<Provider.EventHandler>();
builder.Services.AddSingleton<EventBus>();
builder.Services.AddSingleton<EventListener>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference().WithName("Provider API");//available at {base}/scalar
}

app.UseHttpsRedirection();

app.MapGet("/users", (UserStore userStore) => userStore.GetUsers());
app.MapPost("/users", (UserStore userStore, EventBus eventBus, [FromBody] User user) =>
{
    var newUser = userStore.CreateUser(user);
    eventBus.AddEvent(new UserCreated(newUser.Id, newUser.Name));
});
app.MapGet("/users/{id}", (UserStore userStore, int id) => userStore.GetUserById(id));
app.MapPut("/users", (UserStore userStore, [FromBody]User user) => userStore.UpdateUser(user));
app.MapDelete("/users/{id}", (UserStore userStore, int id) => userStore.DeleteUser(id));




app.MapGet("/health", () => "OK").WithName("Health");


app.Services.GetRequiredService<EventListener>().DoWork();

app.Run();