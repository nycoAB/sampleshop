using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyShopApi.App;
using MyShopApi.App.Mappers;
using MyShopApi.App.Providers;
using MyShopApi.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configs = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>
    ((opt) =>
    {
        opt.UseSqlServer(configs.GetConnectionString("TestConnection"));
    });
var lifeTimeMinutesConfig = configs["Token:AccessExpiryMinutes"];
var lifeTimeMinutes = int.Parse(lifeTimeMinutesConfig);
var tokenSecurityKey = Encoding.UTF8.GetBytes(configs["Token:SecurityKey"]);
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(config =>
    {
        config.SaveToken = true;
        config.RequireHttpsMetadata = false;
        config.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(tokenSecurityKey),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(lifeTimeMinutes)
        };
    });
builder.Services.AddAuthorization();
// Providers
builder.Services.AddScoped<UserManager>();

// Repositories
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();  

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new ProductMapper());
    mc.AddProfile(new CategoryMapper());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
//

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();
app.UseCors(options => options.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();

app.UseAuthorization();

app.Run();
