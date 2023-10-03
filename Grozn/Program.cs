
using Grozn.DAL.Interfaces;
using Grozn.DAL.Repositories;
using Grozn.Services;
using Grozn.Services.Interfaces;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
