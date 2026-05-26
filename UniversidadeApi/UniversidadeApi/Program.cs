using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MimeKit;
using Scalar.AspNetCore;
using Serilog;
using System.Text;
using UniversidadeApi.Application.Services;
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

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<IAlunoRepository, AlunoService>();
builder.Services.AddTransient<IAluno_MateriaRepository, Aluno_MateriaService>();
builder.Services.AddTransient<IMateriaRepository, MateriaService>();
builder.Services.AddTransient<INotaRepository, NotaService>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioService>();

builder.Services.AddDbContext<ConnectionContext>(options => options.UseOracle("Data Source=localhost:1521/xepdb1;User ID=ESCOLA;Password=senha123;Persist Security Info=True; Connect Timeout=3000;"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(op =>
{
    op.RequireHttpsMetadata = false;
    op.SaveToken = true;
    op.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("}bCIx{:,oR1p%(aHq4M$jRu6RC[^b{U?BG{9nA0YF^p")),
        ValidateIssuer = true,
        ValidIssuer = "Api",
        ValidateAudience = true,
        ValidAudience = "Cliente",
        ValidateLifetime = true
    };
});

builder.Services.AddOpenApi("escola", options =>
{
    options.AddDocumentTransformer((documento, contexto, CancellationToken) =>
    {
        documento.Info = new()
        {
            Title = "Projeto API escola.",
            Description = "Desenvolvido em .net 10",
            Version = "0.1"
        };
        documento.Servers = 
        [
            new() {Url = "https://localhost:7245", Description = "Servidor local"}
        ];
        documento.ExternalDocs = new()
        {
            Description = "Documentação externa",
            Url = new Uri("https://youtube.com.br")
        };
        return Task.CompletedTask;
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/doc/{documentName}.json");

    app.MapScalarApiReference("/ide/scalar", options =>
    {
        options.Title = "Api escola";
        options.AddDocument("escola", "Api escola");
        options.WithOpenApiRoutePattern("/doc/{documentName}.json");
        //Custumização
        options.WithTheme(ScalarTheme.BluePlanet);
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();