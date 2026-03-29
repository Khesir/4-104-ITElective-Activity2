using MySql.Data.MySqlClient;
using System.Configuration;

namespace _4_104_ITElective_Activity2.Core.Database
{
    /// <summary>
    /// Base class for all Datastores.
    /// Subclasses contain raw SQL (INSERT / SELECT / UPDATE / DELETE).
    /// Repositories use Datastores — they never write SQL themselves.
    /// </summary>
    public abstract class Datastore
    {
        private readonly string _connectionString;
        
        protected Datastore()
        {
            _connectionString = ConfigurationManager
                .ConnectionStrings["MySqlConnection"]
                .ConnectionString;
        }

        /// <summary>Opens and returns a ready-to-use connection. Caller must dispose it.</summary>
        protected MySqlConnection GetConnection()
        {
            var conn = new MySqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

        protected async Task<MySqlConnection> GetConnectionAsync()
        {
            var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();
            return conn;
        }
    }
}
