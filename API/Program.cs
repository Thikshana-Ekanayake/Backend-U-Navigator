using MongoDB.Driver;
using System.Security.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using u_navigator_backend.Infrastructure.Configurations;
using u_navigator_backend.Infrastructure;
using u_navigator_backend.Infrastructure.Repositories;
using u_navigator_backend.Application.Interfaces;
using u_navigator_backend.Application.Services;
using System.Text;
using u_navigator_backend.Infrastructure.Repositories.Interfaces;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("API/appsettings.json", optional: false, reloadOnChange: true);


// Configure MongoDB settings
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBContext>();

// JWT Authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register dependencies
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IStudentRepository, StudentRepository>();
builder.Services.AddSingleton<IConsultantRepository, ConsultantRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

// Enable Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

app.Run();
