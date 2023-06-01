using Microsoft.EntityFrameworkCore;
using Spg.TennisBooking.Application.Services;
using Spg.TennisBooking.Configurations.v2;
using Spg.TennisBooking.Domain.Interfaces;
//using Spg.TennisBooking.Infrastructure;
using Spg.TennisBooking.Repository.Repositories.v1;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager Conf = builder.Configuration;

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
string useDb = Conf.GetSection("ConnectionStrings").GetValue<string>("UseDb");

// Add services to the container.
builder.Services.AddControllersWithViews();
// builder.Services.AddTransient<IProductService, ProductService>();
// builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.ConfigureDB(connectionString, useDb);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
