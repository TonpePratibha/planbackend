using DataAcessLayer;
using DataAcessLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("plandata"),
    sqlOptions => sqlOptions.EnableRetryOnFailure()));

builder.Services.AddScoped<Iplanrepo,PlanRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add Swagger for API documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeManagement", Version = "v1" });
});

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");

// Enable Swagger UI for API documentation
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Generates the Swagger JSON documentation
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeManagement API v1");
        c.RoutePrefix = string.Empty;  // This makes Swagger available at the root (e.g., http://localhost:5000)
    });
}




// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
