using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Reader;
using Serilog;
using Serilog.Events;
using System.Configuration;
using System.Text.Json.Serialization;
using UniversidadeApi.Infrastucture.Context;
using UniversidadeApi.Infrastucture.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var configuration_logs = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Information()
    //.WriteTo.File("C:/Users/p0600861/Desktop/logs/teste.txt", rollingInterval: RollingInterval.Day)
    .ReadFrom.Configuration(configuration_logs));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddTransient<IAlunoRepository, AlunoRepository>();
builder.Services.AddTransient<IAluno_MateriaRepository, Aluno_MateriaRepository>();
builder.Services.AddTransient<IMateriaRepository, MateriaRepository>();
builder.Services.AddTransient<INotaRepository, NotaRepository>();

//Colocar connection string dentro do AppSettings.JSON
builder.Services.AddDbContext<ConnectionContext>(options => options.UseOracle("Data Source=localhost:1521/xepdb1;User ID=UNIVERSIDADE;Password=senha123;Persist Security Info=True; Connect Timeout=3000;"));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UNIVERSIDADE",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Universidade Api V1");});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
