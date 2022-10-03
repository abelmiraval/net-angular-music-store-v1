using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicStore.API.Profiles;
using MusicStore.DataAccess;
using MusicStore.Entities.Configurations;
using MusicStore.Services;
using System.Text;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MusicStore.API.HealthChecks;
using MusicStore.Services.Implementations;
using MusicStore.Services.Interfaces;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

var corsConfiguration = "MusicStoreAPI";

/*
 * LEVELS
 * 1. INFORMATION
 * 2. WARNING
 * 3. ERROR
 * 4. FATAL
 */

if (builder.Environment.IsDevelopment())
{
    var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console() // Sink es el destino de donde se guardan los mensajes.
        .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("MusicStoreDB"),
            new MSSqlServerSinkOptions
            {
                AutoCreateSqlTable = true,
                TableName = "ApiLogs"
            }, restrictedToMinimumLevel: LogEventLevel.Warning)
        .CreateLogger();

    logger.Information("Hola soy Serilog");

    builder.Host.ConfigureLogging(options =>
    {
        //options.ClearProviders();
        options.AddSerilog(logger);
    });
}

builder.Services.AddCors(setup =>
{
    setup.AddPolicy(corsConfiguration, x =>
    {
        //x.WithOrigins("localhost", "musicstore.azurewebsites.net")
        x.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddAutoMapper(options => options.AddProfile<AutoMapperProfiles>());

if (builder.Environment.IsDevelopment())
    builder.Services.AddTransient<IFileUploader, FileUploader>();
else
    builder.Services.AddTransient<IFileUploader, AzureBlobStorageUploader>();

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

builder.Services.AddHealthChecks()
    .AddCheck("MusicStoreAPI", _ => HealthCheckResult.Healthy(), new[] { "servicio" })
    .AddTypeActivatedCheck<DiskHealthCheck>("Almacenamiento", HealthStatus.Healthy, new[] { "servicio" }, builder.Configuration)
    .AddTypeActivatedCheck<PingHealthCheck>("Google", HealthStatus.Healthy, new[] { "internet" }, "google.com")
    .AddTypeActivatedCheck<PingHealthCheck>("Azure", HealthStatus.Healthy, new[] { "internet" }, "azure.com")
    .AddTypeActivatedCheck<PingHealthCheck>("Host desconocido", HealthStatus.Healthy, new[] { "internet" }, "mocosoft.com.pe")
    .AddDbContextCheck<MusicStoreDbContext>("EF Core", null, new[] { "basedatos" });

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
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// HABILITAMOS EL CORS (EL FRONT-END TE LO AGRADECERA)
app.UseCors(corsConfiguration);

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        Predicate = x => x.Tags.Contains("servicio")
    });

    endpoints.MapHealthChecks("/health/db", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        Predicate = x => x.Tags.Contains("basedatos")
    });

    endpoints.MapHealthChecks("/health/externos", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        Predicate = x => x.Tags.Contains("internet")
    });


});

app.MapControllers();

app.Run();