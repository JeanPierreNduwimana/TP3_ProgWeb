using Microsoft.EntityFrameworkCore;
using FlappyBird_WebAPI.Data;
using Microsoft.AspNetCore.Identity;
using FlappyBird_WebAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FlappyBird_WebAPIContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlappyBird_WebAPIContext") ?? throw new InvalidOperationException("Connection string 'FlappyBird_WebAPIContext' not found."));
    options.UseLazyLoadingProxies();
});
builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<FlappyBird_WebAPIContext>(); //il faut que ca soit avant AddAuthentificiation
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = "http://localhost:4200",
        ValidIssuer = "http://localhost:7151",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Looo000ongue Phrase SinoN Ca nem arche passsAAAssssaS !"))
    };
});

// Add services to the container.
builder.Services.AddScoped<ScoreService>();
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

builder.Services.Configure<IdentityOptions>(options => options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Allow all");

app.UseHttpsRedirection();

app.UseAuthentication();//il faut que ca soit avant UseAuthorization

app.UseAuthorization();

app.MapControllers();

app.Run();
