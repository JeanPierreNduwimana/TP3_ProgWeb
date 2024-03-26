using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FlappyBird_WebAPI.Data;
using Microsoft.AspNetCore.Identity;
using FlappyBird_WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FlappyBird_WebAPIContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlappyBird_WebAPIContext") ?? throw new InvalidOperationException("Connection string 'FlappyBird_WebAPIContext' not found."));
    options.UseLazyLoadingProxies();
});


// Add services to the container.
builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<FlappyBird_WebAPIContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow all", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Allow all");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
