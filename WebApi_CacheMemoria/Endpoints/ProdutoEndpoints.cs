using WebApi_CacheMemoria.Models;

namespace WebApi_CacheMemoria.Endpoints
{
    public static class ProdutoEndpoints
    {
        private static readonly List<Produto> produtos = new()
        {
            new Produto
            {
                Id = 1,
                Nome = "Notebook",
                Preco = 3500
            },
            new Produto
            {
                Id = 2,
                Nome = "Mouse",
                Preco = 100
            }
        };

        public static void MapProdutoEndpoints(this WebApplication app)
        {
            app.MapGet("/produtos", () =>
            {
                return Results.Ok(produtos);
            });

            app.MapGet("/produtos/{id}", (int id) =>
            {
                var produto = produtos.FirstOrDefault(x => x.Id == id);

                return produto is null
                    ? Results.NotFound()
                    : Results.Ok(produto);
            });

            app.MapPost("/produtos", (Produto produto) =>
            {
                produto.Id = produtos.Count + 1;

                produtos.Add(produto);

                return Results.Created(
                    $"/produtos/{produto.Id}",
                    produto
                );
            });

            app.MapPut("/produtos/{id}", (int id, Produto produtoAtualizado) =>
            {
                var produto = produtos.FirstOrDefault(x => x.Id == id);

                if (produto is null)
                    return Results.NotFound();

                produto.Nome = produtoAtualizado.Nome;
                produto.Preco = produtoAtualizado.Preco;

                return Results.Ok(produto);
            });

            app.MapDelete("/produtos/{id}", (int id) =>
            {
                var produto = produtos.FirstOrDefault(x => x.Id == id);

                if (produto is null)
                    return Results.NotFound();

                produtos.Remove(produto);

                return Results.NoContent();
            });
        }
    }
}