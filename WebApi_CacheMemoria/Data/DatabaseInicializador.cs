namespace WebApi_CacheMemoria.Data
{
    public class DatabaseInicializador
    {
        private readonly Database _database;

        public DatabaseInicializador(Database database)
        {
            _database = database;
        }

        public void Initialize()
        {
            using var connection = _database.GetConnection();

            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = """
            CREATE TABLE IF NOT EXISTS produtos
            (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                nome TEXT NOT NULL,
                preco REAL NOT NULL,
                estoque INTEGER NOT NULL
            );
            """;

            command.ExecuteNonQuery();
        }
    }
}