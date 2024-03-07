using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using MasterArtsWeb;
using MasterArtsWeb.Data;

using MasterArtsWeb.Pages;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MyDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddSingleton<LanguageService>();

builder.Services.AddScoped<OrderService>();


builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IOrderEmailSender, EmailSender>();
builder.Services.AddTransient<DataInitializer>();
builder.Services.AddHttpClient();
builder.Services.AddTransient<IUser, UserFactory>();


builder.Services.AddScoped<UserManager<IdentityUser>>();

// Registrera SeedDataService




var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();