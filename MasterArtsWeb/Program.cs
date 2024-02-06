using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MasterArtsLibrary.Data;
using MasterArtsLibrary.Models;
using MasterArtsLibrary.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using MasterArtsWeb;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MyDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<LanguageService>();
builder.Services.AddSingleton<CurrencyFactory>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddScoped<IOrderEmailSender, EmailSender>();
builder.Services.AddHttpClient();


// Add services to the container.
builder.Services.AddRazorPages();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
