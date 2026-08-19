using WebApi_CacheMemoria.BLL;
using WebApi_CacheMemoria.Models;

namespace MinhaApi.Endpoints;

public static class ProdutoEndpoints
{
    public static void MapProdutoEndpoints(
        this WebApplication app)
    {
        var group = app.MapGroup("/produtos");

        group.MapGet("/", (ProdutoBLL produtoBLL) =>
        {
            var produtos = produtoBLL.Listar();

            return Results.Ok(produtos);
        });

        group.MapGet("/{id}", (int id, ProdutoBLL produtoBLL) =>
        {
            var produto = produtoBLL.BuscarPorId(id);

            return produto is null ? Results.NotFound() : Results.Ok(produto);
        });

        group.MapPost("/", (Produto produto, ProdutoBLL produtoBLL) =>
        {
            var id = produtoBLL.Inserir(produto);

            produto.Id = id;

            return Results.Created($"/produtos/{id}", produto);
        });

        group.MapPut("/{id}", (int id, Produto produto, ProdutoBLL produtoBLL) =>
        {
            produtoBLL.Atualizar(id, produto);

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id, ProdutoBLL produtoBLL) =>
        {
            produtoBLL.Excluir(id);

            return Results.NoContent();
        });
    }
}