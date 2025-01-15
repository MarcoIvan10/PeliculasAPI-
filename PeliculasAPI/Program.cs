using PeliculasAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Caché
builder.Services.AddOutputCache(opciones =>
{
    opciones.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(60); 
});


// Servicio de Repositorio en Memoria 
builder.Services.AddSingleton<IRepositorio, RepositorioEnMemoria>();

builder.Services.AddTransient<ServiciosTransient>();
builder.Services.AddScoped<ServiciosScoped>();
builder.Services.AddSingleton<ServiciosSingleton>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseOutputCache(); //

app.UseAuthorization();

app.MapControllers();

app.Run();
