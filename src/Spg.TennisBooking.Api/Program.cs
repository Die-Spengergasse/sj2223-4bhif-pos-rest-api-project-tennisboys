using Microsoft.EntityFrameworkCore;
using Spg.TennisBooking.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

DbContextOptions options = new DbContextOptionsBuilder()
    .UseSqlite("Data Source=TennisBooking.db")
    .Options;

TennisBookingContext db = new TennisBookingContext(options);
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
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
app.UseStaticFiles();

app.UseAuthorization();

// app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Swagger Configuration
builder.Services.AddSwaggerGen(s =>
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
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

app.Run();