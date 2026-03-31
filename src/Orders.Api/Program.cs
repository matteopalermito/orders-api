using Microsoft.EntityFrameworkCore;
using Orders.Api.Data;
using Orders.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add servicesuilder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=orders.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// DB init
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/orders", async (AppDbContext db) => await db.Orders.ToListAsync())
   .WithName("GetOrders")
   .WithOpenApi();

app.MapGet("/api/orders/{id:int}", async (int id, AppDbContext db) => await db.Orders.FindAsync(id))
   .WithName("GetOrderById")
   .WithOpenApi();

app.MapPost("/api/orders", async (Order order, AppDbContext db) =>
{
    order.CreatedUtc = DateTime.UtcNow;
    db.Orders.Add(order);
    await db.SaveChangesAsync();
    return Results.Created($"/api/orders/{order.Id}", order);
}).WithName("CreateOrder").WithOpenApi();

app.MapPut("/api/orders/{id:int}", async (int id, Order input, AppDbContext db) =>
{
    var order = await db.Orders.FindAsync(id);
    if (order is null) return Results.NotFound();

    order.Customer = input.Customer;
    order.Total = input.Total;
    order.Status = input.Status;
    await db.SaveChangesAsync();
    return Results.NoContent();
}).WithName("UpdateOrder").WithOpenApi();

app.MapDelete("/api/orders/{id:int}", async (int id, AppDbContext db) =>
{
    var order = await db.Orders.FindAsync(id);
    if (order is null) return Results.NotFound();
    db.Remove(order);
    await db.SaveChangesAsync();
    return Results.NoContent();
}).WithName("DeleteOrder").WithOpenApi();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run();
