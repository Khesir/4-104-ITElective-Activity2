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
                INSERT INTO transactions (user_id, total_amount, created_at)
                VALUES (@userId, @totalAmount, @createdAt);
                SELECT LAST_INSERT_ID();";

            using var conn = GetConnection();
            using var cmd  = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userId",      (object?)t.userId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@totalAmount", t.totalAmount);
            cmd.Parameters.AddWithValue("@createdAt",   t.createdAt);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public Transaction? SelectById(int id)
        {
            const string sql = @"
                SELECT id, user_id, total_amount, created_at
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
                SELECT id, user_id, total_amount, created_at
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
            id        = r.GetInt32("id"),
            userId    = r.IsDBNull(r.GetOrdinal("user_id")) ? null : r.GetInt32("user_id"),
            createdAt = r.GetDateTime("created_at"),
        };
    }
}
