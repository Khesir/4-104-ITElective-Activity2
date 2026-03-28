using _4_104_ITElective_Activity2.Core.Database;
using MySql.Data.MySqlClient;

namespace _4_104_ITElective_Activity2.modules.transaction
{
    /// <summary>All SQL for the transactions table lives here.</summary>
    public class TransactionDatastore : Datastore
    {
        /// <summary>Inserts a transaction header and returns the new id.</summary>
        public int Insert(Transaction t)
        {
            const string sql = @"
                INSERT INTO transactions (total_amount, created_at)
                VALUES (@totalAmount, @createdAt);
                SELECT LAST_INSERT_ID();";

            using var conn = GetConnection();
            using var cmd  = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@totalAmount", t.totalAmount);
            cmd.Parameters.AddWithValue("@createdAt",   t.createdAt);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public Transaction? SelectById(int id)
        {
            const string sql = @"
                SELECT id, total_amount, created_at
                FROM transactions
                WHERE id = @id
                LIMIT 1";

            using var conn   = GetConnection();
            using var cmd    = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? Map(reader) : null;
        }

        public List<Transaction> SelectAll()
        {
            const string sql = @"
                SELECT id, total_amount, created_at
                FROM transactions
                ORDER BY created_at DESC";

            using var conn   = GetConnection();
            using var cmd    = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            var list = new List<Transaction>();
            while (reader.Read()) list.Add(Map(reader));
            return list;
        }

        private static Transaction Map(MySqlDataReader r) => new Transaction
        {
            id          = r.GetInt32("id"),
            createdAt   = r.GetDateTime("created_at"),
        };
    }
}
