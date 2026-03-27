using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using TaskTracker.Server.Data;
using TaskTracker.Server.Models.DbContexts;
using TaskTracker.Server.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");

builder.Services.AddDbContextFactory<TaskDbContext>(options 
                    => options.UseNpgsql(connectionString, 
                    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "tasks")));

builder.Services.AddDbContextFactory<UserDbContext>(options 
                    => options.UseNpgsql(connectionString,
                    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "users")));

builder.Services.AddScoped<TaskRepository>();
builder.Services.AddScoped<UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
