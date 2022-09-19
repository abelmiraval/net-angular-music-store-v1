using Microsoft.EntityFrameworkCore;
using MusicStore.API.Profiles;
using MusicStore.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(options => options.AddProfile<AutoMapperProfiles>());

// Add services to the container.
builder.Services.AddDbContext<MusicStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MusicStoreDB"));
    // Mostrar el detalle de EF Core
    //options.LogTo(Console.WriteLine, LogLevel.Trace);

    // SOLO Habilitar en modo Desarrollo
    options.EnableSensitiveDataLogging();

    // Utiliza el AsNoTracking por default en todos los querys de Seleccion
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
