using Business.Announcement.Services;
using Business.Users.Services;
using Core.Contracts;
using Core.Repositories;
using DataAccess.Announcement.Repository;
using DataAccess.Database.Context;
using DataAccess.Database.Models;
using DataAccess.Users.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OlxContext>(options =>
      options.UseSqlServer(builder.Configuration.GetConnectionString("OlxApp")));

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IAdsService,AdsService>();
builder.Services.AddTransient<IAdRepository, AdsRepository>();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase= false;
    options.Password.RequireLowercase= false;
})
 .AddEntityFrameworkStores<OlxContext>()
 .AddDefaultTokenProviders();

builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions,BasicAuthenticationHandler>("BasicAuthentication",null);
builder.Services.AddAuthorization();

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

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();