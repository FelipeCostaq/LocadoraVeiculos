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
using LocadoraVeiculos.Infrastructure.AzureBlobStorage;
using LocadoraVeiculos.Infrastructure.Data;
using LocadoraVeiculos.Infrastructure.Identity;
using LocadoraVeiculos.Infrastructure.Repositories;
using LocadoraVeiculos.Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontends",
        policy =>
        {
            policy.WithOrigins("https://locadora-veiculos-flax.vercel.app", "http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddHideEndpointsIdentityFilter();

builder.Services.AddControllers();

builder.Services.AddDbContext<LocadoraContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
});

builder.Services
    .AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<LocadoraContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; 
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

// Categorias Veiculo dependency injection
builder.Services.AddScoped<ICategoriaVeiculo, RepositoryCategoriaVeiculo>();
builder.Services.AddScoped<InterfaceCategoriaVeiculoApp, AppCategoriaVeiculo>();
builder.Services.AddScoped<IServiceCategoriaVeiculo, ServiceCategoriaVeiculo>();

// Veiculo dependency injection
builder.Services.AddScoped<IVeiculo, RepositoryVeiculo>();
builder.Services.AddScoped<InterfaceVeiculoApp, AppVeiculo>();
builder.Services.AddScoped<IServiceVeiculo, ServiceVeiculo>();

// Veiculo Alocado dependency injection
builder.Services.AddScoped<IVeiculoAlocado, RepositoryVeiculoAlocado>();
builder.Services.AddScoped<InterfaceVeiculoAlocadoApp, AppVeiculoAlocado>();
builder.Services.AddScoped<IServiceVeiculoAlocado, ServiceVeiculoAlocado>();

// Blob Storage dependency injection
builder.Services.AddScoped<IServiceStorage, AzureBlobStorageService>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LocadoraContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontends");

var routesBlocked = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
{
    "/auth/register",
    "/auth/refresh",
    "/auth/confirmEmail",
    "/auth/resendConfirmationEmail",
    "/auth/forgotPassword",
    "/auth/resetPassword",
    "/auth/manage/2fa",
};

app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value;

    if (path != null && routesBlocked.Contains(path))
    {
        context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
        return; 
    }

    await next(context);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}
    
app.UseAuthentication();
app.UseAuthorization();

app.MapUserEndpoints();
app.MapControllers();

app.Run();
