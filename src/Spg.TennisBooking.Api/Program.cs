using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Spg.TennisBooking.Application.Services;
using Spg.TennisBooking.Configurations;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Infrastructure;
using Spg.TennisBooking.Repository.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager Conf = builder.Configuration;

// Add services to the container.


//Add db
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//Clear db
DbContextOptions options = new DbContextOptionsBuilder()
    .UseSqlite(connectionString)
    .Options;

TennisBookingContext db = new TennisBookingContext(options);
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

//builder.Services.AddDbContext<TennisBookingContext>(options => options.UseSqlite("Data Source=TennisBooking.db"));
builder.Services.ConfigureSqLite(connectionString);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Interfaces
builder.Services.ConfigureInterfaces();

//JWT
/*string jwtSecret = Configuration["AppSettings:Secret"] ?? AuthService.GenerateRandom(1024);
builder.Services.AddJwtAuthentication(jwtSecret, setDefault: true);
builder.Services.AddScoped<AuthService>(services =>
    new AuthService(jwtSecret));*/
string JWT = Conf.GetSection("JWT").GetValue<string>("JWTSecret");
Console.WriteLine(JWT);
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(auth =>
{
    auth.RequireHttpsMetadata = false;
    auth.SaveToken = true;
    auth.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWT)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//TODO: Stripe API Key

//Swagger Configuration
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo()
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
    });
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token here"
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

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

//API Versioning
builder.Services.AddApiVersioning();

app.Run();