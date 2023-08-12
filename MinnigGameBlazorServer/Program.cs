///Sources I used: https://github.com/iso8859/AspNetCoreAuthMultiLang/blob/main/Pages/LoadUserContext.cs

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MinnigGameBlazorServer.Data;
using MinnigGameBlazorServer.Services;
using GameCorpLib;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

GameControler gameControler = new GameControler();
// Add services to the container
builder.Services.AddAuthorizationCore();
builder.Services.AddOptions();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<IUserMaintainer>(gameControler);
builder.Services.AddSingleton<GameControler>(gameControler);
builder.Services.AddScoped<UserStateMaintainer>();
builder.Services.AddScoped<EventAgregator<Player>>();



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
