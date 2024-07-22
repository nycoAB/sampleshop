using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyShopApi.App;
using MyShopApi.App.Mappers;
using MyShopApi.App.Providers;
using MyShopApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configs = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>
    ((opt) =>
    {
        opt.UseSqlServer(configs.GetConnectionString("Nakhll"));
    });
// Providers
builder.Services.AddScoped<UserManager>();

// Repositories
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new ProductMapper());
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
