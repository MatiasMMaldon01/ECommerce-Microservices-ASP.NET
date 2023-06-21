using Application.Catalog.Interfaces;
using Application.Catalog.Service;
using Domain.Catalog.Interfaces;
using Infraestructure.Catalog.Repository;
using Microsoft.Extensions.Options;
using Infraestructure.Catalog.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DBSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IDBSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<DBSettings>>().Value);

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
