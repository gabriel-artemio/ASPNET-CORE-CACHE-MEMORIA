using Microsoft.Data.Sqlite;

namespace WebApi_CacheMemoria.Data
{
    public class Database
    {
        private readonly string _connectionString;

        public Database(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new Exception("Connection string não encontrada.");
        }
        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(_connectionString);
        }
    }
}