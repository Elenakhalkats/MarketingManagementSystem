using MarketingManagementSystem.Infrastucture.Contexts;
using MarketingManagementSystem.Infrastucture.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;
using MarketingManagementSystem.Application.Exceptions;
using MarketingManagementSystem.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDistributorsRepository, DistributorsRepository>();
builder.Services.AddScoped<IProductsSalesRepository, ProductsSalesRepository>();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMediatR(typeof(IDistributorsRepository).Assembly);
builder.Services.AddMediatR(typeof(IProductsSalesRepository).Assembly);

builder.Services.AddDbContext<
    MarketingManagementSystemContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketingManagementSystemDB")); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<AppExceptionHandler>();
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
