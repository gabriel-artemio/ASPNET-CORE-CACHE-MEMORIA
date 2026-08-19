using Microsoft.Extensions.Caching.Memory;
using WebApi_CacheMemoria.BLL;
using WebApi_CacheMemoria.Models;

namespace MinhaApi.Endpoints;

public static class ProdutoEndpoints
{
    public static void MapProdutoEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/produtos");

        group.MapGet("/", (ProdutoBLL produtoBLL, IMemoryCache cache) =>
        {
            const string cacheKey = "produtos";

            if (!cache.TryGetValue(cacheKey, out List<Produto>? produtos))
            {
                produtos = produtoBLL.Listar();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                cache.Set(cacheKey, produtos, cacheEntryOptions);
            }

            return Results.Ok(produtos);
        });

        group.MapGet("/{id}", (int id, ProdutoBLL produtoBLL, IMemoryCache cache) =>
        {
            var cacheKey = $"produto:{id}";

            if (!cache.TryGetValue(cacheKey, out Produto? produto))
            {
                produto = produtoBLL.BuscarPorId(id);

                if (produto is null)
                {
                    return Results.NotFound();
                }

                cache.Set(cacheKey, produto, TimeSpan.FromMinutes(30));
            }

            return Results.Ok(produto);
        });

        group.MapPost("/", (Produto produto, ProdutoBLL produtoBLL, IMemoryCache cache) =>
        {
            var id = produtoBLL.Inserir(produto);

            cache.Remove("produtos");

            produto.Id = id;

            return Results.Created($"/produtos/{id}", produto);
        });

        group.MapPut("/{id}", (int id, Produto produto, ProdutoBLL produtoBLL, IMemoryCache cache) =>
        {
            produtoBLL.Atualizar(id, produto);

            cache.Remove("produtos");

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id, ProdutoBLL produtoBLL, IMemoryCache cache) =>
        {
            produtoBLL.Excluir(id);

            cache.Remove("produtos");

            return Results.NoContent();
        });
    }
}