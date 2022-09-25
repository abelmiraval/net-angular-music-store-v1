using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicStore.API.Profiles;
using MusicStore.DataAccess;
using MusicStore.Entities.Configurations;
using MusicStore.Services;
using System.Text;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

/*
 * LEVELS
 * 1. INFORMATION
 * 2. WARNING
 * 3. ERROR
 * 4. FATAL
 */

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console() // Sink es el destino de donde se guardan los mensajes
    .CreateLogger();

builder.Host.ConfigureLogging(options =>
{
    //options.ClearProviders();
    options.AddSerilog(logger);
});

logger.Information("Hola soy Serilog");

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddAutoMapper(options => options.AddProfile<AutoMapperProfiles>());
builder.Services.AddDependencies();

// Add services to the container.
builder.Services.AddDbContext<MusicStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MusicStoreDB"));
    // Mostrar el detalle del EF Core.
    //options.LogTo(Console.WriteLine, LogLevel.Trace);

    // SOLO Habilitar en modo Desarrollo
    options.EnableSensitiveDataLogging();

    // Utiliza el AsNoTracking por default en todos los querys de Seleccion
    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddIdentity<MusicStoreUserIdentity, IdentityRole>(setup =>
{
    setup.Password.RequireNonAlphanumeric = false;
    setup.Password.RequiredUniqueChars = 0;
    setup.Password.RequireUppercase = false;
    setup.Password.RequireLowercase = false;
    setup.Password.RequireDigit = false;
    setup.Password.RequiredLength = 8;

    setup.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<MusicStoreDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var key = Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:SigningKey"));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var app = builder.Build();

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
