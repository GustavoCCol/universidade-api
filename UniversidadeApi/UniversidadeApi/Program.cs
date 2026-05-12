using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
    .ReadFrom.Configuration(configuration_logs));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddTransient<IAlunoRepository, AlunoRepository>();
builder.Services.AddTransient<IAluno_MateriaRepository, Aluno_MateriaRepository>();
builder.Services.AddTransient<IMateriaRepository, MateriaRepository>();
builder.Services.AddTransient<INotaRepository, NotaRepository>();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Universidade Api V1");});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
