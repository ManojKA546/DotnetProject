using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System.Data; // Required for IDbConnection
using Microsoft.Data.SqlClient; // Required for SqlConnection (to use with Dapper)
using Dapper; 
//using System.Collections.Generic;
//using Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Register Dapper service
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new SqlConnection(config.GetConnectionString("DefaultConnection"));
});
// Register ProductDB as a service
builder.Services.AddScoped<ProductDB>();
builder.Services.AddScoped<CategoryDB>(); // Add this line to register ProductDB

// Add services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
/*
// Add a simple GET route to fetch data from the database
app.MapGet("/categories", async (IDbConnection dbConnection) =>
{
    // SQL query to fetch all categories
    var categories = await dbConnection.QueryAsync<Category>("SELECT * FROM Category");

    return Results.Ok(categories); // Return the categories as a response
});

// Add a simple GET route to fetch data from the database
app.MapGet("/product", async (IDbConnection dbConnection) =>
{
    // SQL query to fetch all categories
    var product = await dbConnection.QueryAsync<Product>("SELECT * FROM Product");

    return Results.Ok(product); // Return the categories as a response
});
*/

// Enable authorization (if applicable)
app.UseAuthorization();

// Use CORS
app.UseCors("AllowAll");

// Map Controllers (for controller-based APIs)
app.MapControllers();

app.Run();

// Category model (adjust as per your database schema)

