using _4_104_ITElective_Activity2.Core.Database;
using MySql.Data.MySqlClient;

namespace _4_104_ITElective_Activity2.modules.item
{
    /// <summary>All SQL for the products table lives here.</summary>
    public class ProductDatastore : Datastore
    {
        public int Insert(Product p)
        {
            const string sql = @"
                INSERT INTO products (name, price, image_path)
                VALUES (@name, @price, @imagePath);
                SELECT LAST_INSERT_ID();";

            using var conn = GetConnection();
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name",      p.name);
            cmd.Parameters.AddWithValue("@price",     p.price);
            cmd.Parameters.AddWithValue("@imagePath", p.imagePath);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public List<Product> SelectAll()
        {
            const string sql = "SELECT id, name, price, image_path FROM products ORDER BY id";

            using var conn = GetConnection();
            using var cmd  = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            var list = new List<Product>();
            while (reader.Read())
            {
                list.Add(Map(reader));
            }
            return list;
        }

        public Product? SelectById(int id)
        {
            const string sql = "SELECT id, name, price, image_path FROM products WHERE id = @id LIMIT 1";

            using var conn = GetConnection();
            using var cmd  = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? Map(reader) : null;
        }

        public void Update(Product p)
        {
            const string sql = @"
                UPDATE products
                SET name = @name, price = @price, image_path = @imagePath
                WHERE id = @id";

            using var conn = GetConnection();
            using var cmd  = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id",        p.id);
            cmd.Parameters.AddWithValue("@name",      p.name);
            cmd.Parameters.AddWithValue("@price",     p.price);
            cmd.Parameters.AddWithValue("@imagePath", p.imagePath);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            const string sql = "DELETE FROM products WHERE id = @id";

            using var conn = GetConnection();
            using var cmd  = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        private static Product Map(MySqlDataReader r) => new Product(
            r.GetString("name"),
            r.GetDouble("price"),
            r.GetString("image_path")
        )
        { id = r.GetInt32("id") };
    }
}
