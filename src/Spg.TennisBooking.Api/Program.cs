using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Spg.TennisBooking.Application.Services;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Infrastructure;
using Spg.TennisBooking.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

DbContextOptions options = new DbContextOptionsBuilder()
    .UseSqlite("Data Source=TennisBooking.db")
    .Options;

TennisBookingContext db = new TennisBookingContext(options);
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

//Add db
builder.Services.AddDbContext<TennisBookingContext>(options => options.UseSqlite("Data Source=TennisBooking.db"));

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Interfaces
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IAuthRepository, AuthRepository>();

//Swagger Configuration
builder.Services.AddSwaggerGen(s => s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() 
    { 
        Title = "TennisBooking", 
        Description = "Tennis Booking Website", 
        Contact = new OpenApiContact() 
        { 
            Name = "Adrian Schauer",
            Email = "info@adrian-schauer.at", 
            Url = new Uri("http://www.spengergasse.at")
        }, 
        License = new OpenApiLicense() 
        { 
            Name = "Schauer-Licence", 
            Url = new Uri("http://www.adrian-schauer.at/license") 
        }, 
        Version = "v1" 
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

// app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();