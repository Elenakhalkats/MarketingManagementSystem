using MarketingManagementSystem.Infrastucture.Contexts;
using MarketingManagementSystem.Infrastucture.Repositories;
using MarketingManagementSystem.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDistributorsRepository, DistributorsRepository>();
builder.Services.AddScoped<IProductsSalesRepository, ProductsSalesRepository>();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMediatR(typeof(IDistributorsRepository).Assembly);
builder.Services.AddMediatR(typeof(IProductsSalesRepository).Assembly);

builder.Services.AddDbContext<MarketingManagementSystemContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketingManagementSystemDB"), b => b.MigrationsAssembly("MarketingManagementSystem.Web"));
});

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
