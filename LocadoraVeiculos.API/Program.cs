using LocadoraVeiculos.Application.Interfaces;
using LocadoraVeiculos.Application.OpenApp;
using LocadoraVeiculos.Domain.Interfaces.Generics;
using LocadoraVeiculos.Domain.Interfaces.InterfaceCategoriaVeiculo;
using LocadoraVeiculos.Domain.Interfaces.InterfaceCliente;
using LocadoraVeiculos.Domain.Interfaces.InterfaceServices;
using LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculo;
using LocadoraVeiculos.Domain.Interfaces.InterfaceVeiculoAlocado;
using LocadoraVeiculos.Domain.Services;
using LocadoraVeiculos.Entities.Entities;
using LocadoraVeiculos.Infrastructure.Data;
using LocadoraVeiculos.Infrastructure.Identity;
using LocadoraVeiculos.Infrastructure.Repositories;
using LocadoraVeiculos.Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontends",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services.AddHideEndpointsIdentityFilter();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LocadoraContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<LocadoraContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Obrigatório para SameSite=None
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});

builder.Services.AddSingleton(typeof(IGenerics<>), typeof(RepositoryGenerics<>));

// Cliente dependency injection
builder.Services.AddScoped<ICliente, RepositoryCliente>();
builder.Services.AddScoped<InterfaceClienteApp, AppCliente>();
builder.Services.AddScoped<IServiceCliente, ServiceCliente>();
builder.Services.AddScoped<ServiceCliente>();

// Categorias Veiculo dependency injection
builder.Services.AddScoped<ICategoriaVeiculo, RepositoryCategoriaVeiculo>();
builder.Services.AddScoped<InterfaceCategoriaVeiculoApp, AppCategoriaVeiculo>();
builder.Services.AddScoped<IServiceCategoriaVeiculo, ServiceCategoriaVeiculo>();
builder.Services.AddScoped<ServiceCategoriaVeiculo>();

// Veiculo dependency injection
builder.Services.AddScoped<IVeiculo, RepositoryVeiculo>();
builder.Services.AddScoped<InterfaceVeiculoApp, AppVeiculo>();
builder.Services.AddScoped<IServiceVeiculo, ServiceVeiculo>();
builder.Services.AddScoped<ServiceVeiculo>();

// Veiculo Alocado dependency injection
builder.Services.AddScoped<IVeiculoAlocado, RepositoryVeiculoAlocado>();
builder.Services.AddScoped<InterfaceVeiculoAlocadoApp, AppVeiculoAlocado>();
builder.Services.AddScoped<IServiceVeiculoAlocado, ServiceVeiculoAlocado>();
builder.Services.AddScoped<ServiceVeiculoAlocado>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontends");

app.UseHttpsRedirection();
    
app.UseAuthentication();
app.UseAuthorization();

app.MapUserEndpoints();
app.MapControllers();

app.Run();
