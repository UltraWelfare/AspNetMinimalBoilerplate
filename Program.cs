using AspNetMinimalBoilerplate.Entities;
using AspNetMinimalBoilerplate.Features.Items;
using AspNetMinimalBoilerplate.Features.Items.CreateItem;
using AspNetMinimalBoilerplate.Features.Items.UpdateItem;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderContext>();
builder.Services.AddScoped<ItemService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/items", (ItemService itemService) => itemService.GetItems());

app.MapPost("/items",
    async (ItemService itemService, [FromBody] CreateItemParameters parameters) =>
    {
        var result = await itemService.CreateItem(parameters);
        return result.Match<IResult>(TypedResults.Ok, error => error.ToHttpResponse());
    });

app.MapPut("/items/{id:int}",
    async (ItemService itemService, int id, [FromBody] UpdateItemParameters parameters) =>
    {
        var result = await itemService.UpdateItem(id, parameters);
        return result.Match<IResult>(TypedResults.Ok, error => error.ToHttpResponse());
    });

app.MapDelete("/items/{id:int}",
    async (ItemService itemService, int id) =>
    {
        var result = await itemService.DeleteItem(id);
        return result.Match<IResult>(TypedResults.Ok, error => error.ToHttpResponse());
    });


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

app.Run();