using Business.Users.Services;
using Core.Contracts;
using Core.Repositories;
using DataAccess.Database.Context;
using DataAccess.Database.Models;
using DataAccess.Users.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OlxContext>(options =>
      options.UseSqlServer(builder.Configuration.GetConnectionString("OlxApp")));

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRepository<User>, UserRepository>();

var app = builder.Build();

//Initilize the database

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<OlxContext>();
    await context.Database.MigrateAsync();
}

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