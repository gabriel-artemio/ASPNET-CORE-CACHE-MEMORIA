using MinhaApi.Endpoints;
using WebApi_CacheMemoria.BLL;
using WebApi_CacheMemoria.DAL;
using WebApi_CacheMemoria.Data;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddSingleton<Database>();
builder.Services.AddSingleton<DatabaseInicializador>();

// DAL
builder.Services.AddScoped<ProdutoDAL>();

// BLL
builder.Services.AddScoped<ProdutoBLL>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

var app = builder.Build();

// Inicializar banco
using (var scope = app.Services.CreateScope())
{
    var databaseInitializer =
        scope.ServiceProvider
            .GetRequiredService<DatabaseInicializador>();

    databaseInitializer.Initialize();
}

app.UseSwagger();

app.UseSwaggerUI();

// Endpoints
app.MapProdutoEndpoints();

app.Run();