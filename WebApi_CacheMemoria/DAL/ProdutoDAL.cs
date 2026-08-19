using WebApi_CacheMemoria.Data;
using WebApi_CacheMemoria.Models;

namespace WebApi_CacheMemoria.DAL
{
    public class ProdutoDAL
    {
        private readonly Database _database;

        public ProdutoDAL(Database database)
        {
            _database = database;
        }

        public List<Produto> Listar()
        {
            var produtos = new List<Produto>();

            using var connection = _database.GetConnection();

            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = """
                SELECT
                    id,
                    nome,
                    preco,
                    estoque
                FROM produtos;
            """;

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                produtos.Add(new Produto
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Preco = reader.GetDecimal(2),
                    Estoque = reader.GetInt32(3)
                });
            }

            return produtos;
        }

        public Produto? BuscarPorId(int id)
        {
            using var connection = _database.GetConnection();

            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = """
                SELECT
                    id,
                    nome,
                    preco,
                    estoque
                FROM produtos
                WHERE id = @id;
            """;

            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Produto
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Preco = reader.GetDecimal(2),
                    Estoque = reader.GetInt32(3)
                };
            }

            return null;
        }

        public int Inserir(Produto produto)
        {
            using var connection = _database.GetConnection();

            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = """
                INSERT INTO produtos
                (
                    nome,
                    preco,
                    estoque
                )
                VALUES
                (
                    @nome,
                    @preco,
                    @estoque
                );

                SELECT last_insert_rowid();
            """;

            command.Parameters.AddWithValue("@nome", produto.Nome);
            command.Parameters.AddWithValue("@preco", produto.Preco);
            command.Parameters.AddWithValue("@estoque", produto.Estoque);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public void Atualizar(Produto produto)
        {
            using var connection = _database.GetConnection();

            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = """
                UPDATE produtos
                SET
                    nome = @nome,
                    preco = @preco,
                    estoque = @estoque
                WHERE id = @id;
            """;

            command.Parameters.AddWithValue("@id", produto.Id);
            command.Parameters.AddWithValue("@nome", produto.Nome);
            command.Parameters.AddWithValue("@preco", produto.Preco);
            command.Parameters.AddWithValue("@estoque", produto.Estoque);

            command.ExecuteNonQuery();
        }

        public void Excluir(int id)
        {
            using var connection = _database.GetConnection();

            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = """
                DELETE FROM produtos
                WHERE id = @id;
            """;

            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }
}