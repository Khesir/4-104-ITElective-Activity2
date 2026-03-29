using _4_104_ITElective_Activity2.Core.Database;
using MySql.Data.MySqlClient;

namespace _4_104_ITElective_Activity2.modules.transactionItem
{
    /// <summary>All SQL for the transaction_items table lives here.</summary>
    public class TransactionItemDatastore : Datastore
    {
        /// <summary>Bulk-inserts all cart items under a saved transaction id.</summary>
        public void InsertAll(int transactionId, List<TransactionItem> items)
        {
            if (items.Count == 0) return;

            const string sql = @"
                INSERT INTO transaction_items
                    (transaction_id, product_id, name, cup_size, price, quantity, total_price)
                VALUES
                    (@transactionId, @productId, @name, @cupSize, @price, @quantity, @totalPrice)";

            using var conn = GetConnection();
            foreach (var item in items)
            {
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@transactionId", transactionId);
                cmd.Parameters.AddWithValue("@productId",     item.itemId);
                cmd.Parameters.AddWithValue("@name",          item.name);
                cmd.Parameters.AddWithValue("@cupSize",       TransactionItem.cupSizeToString(item.cupSize));
                cmd.Parameters.AddWithValue("@price",         item.price);
                cmd.Parameters.AddWithValue("@quantity",      item.quantity);
                cmd.Parameters.AddWithValue("@totalPrice",    item.totalPrice);
                cmd.ExecuteNonQuery();
            }
        }

        public List<TransactionItem> SelectByTransactionId(int transactionId)
        {
            const string sql = @"
                SELECT id, product_id, name, cup_size, price, quantity, total_price
                FROM transaction_items
                WHERE transaction_id = @transactionId
                ORDER BY id";

            using var conn   = GetConnection();
            using var cmd    = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@transactionId", transactionId);

            using var reader = cmd.ExecuteReader();
            var list = new List<TransactionItem>();
            while (reader.Read()) list.Add(Map(reader));
            return list;
        }

        public async Task InsertAllAsync(int transactionId, List<TransactionItem> items)
        {
            if (items.Count == 0) return;

            const string sql = @"
                INSERT INTO transaction_items
                    (transaction_id, product_id, name, cup_size, price, quantity, total_price)
                VALUES
                    (@transactionId, @productId, @name, @cupSize, @price, @quantity, @totalPrice)";

            using var conn = await GetConnectionAsync();
            foreach (var item in items)
            {
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@transactionId", transactionId);
                cmd.Parameters.AddWithValue("@productId",     item.itemId);
                cmd.Parameters.AddWithValue("@name",          item.name);
                cmd.Parameters.AddWithValue("@cupSize",       TransactionItem.cupSizeToString(item.cupSize));
                cmd.Parameters.AddWithValue("@price",         item.price);
                cmd.Parameters.AddWithValue("@quantity",      item.quantity);
                cmd.Parameters.AddWithValue("@totalPrice",    item.totalPrice);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<TransactionItem>> SelectByTransactionIdAsync(int transactionId)
        {
            const string sql = @"
                SELECT id, product_id, name, cup_size, price, quantity, total_price
                FROM transaction_items
                WHERE transaction_id = @transactionId
                ORDER BY id";

            using var conn   = await GetConnectionAsync();
            using var cmd    = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@transactionId", transactionId);

            using var reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();
            var list = new List<TransactionItem>();
            while (await reader.ReadAsync()) list.Add(Map(reader));
            return list;
        }

        private static TransactionItem Map(MySqlDataReader r) => new TransactionItem
        {
            id       = r.GetInt32("id"),
            itemId   = r.GetInt32("product_id"),
            name     = r.GetString("name"),
            cupSize  = TransactionItem.fromString(r.GetString("cup_size")),
            price    = r.GetDouble("price"),
            quantity = r.GetInt32("quantity"),
        };
    }
}
