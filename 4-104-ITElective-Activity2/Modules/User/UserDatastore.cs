using _4_104_ITElective_Activity2.Core.Database;
using MySql.Data.MySqlClient;

namespace _4_104_ITElective_Activity2.Modules.User
{
    /// <summary>All SQL for the users table lives here.</summary>
    public class UserDatastore : Datastore
    {
        /// <summary>
        /// Returns a User row whose username matches and whose stored password_hash
        /// equals SHA2(@password, 256). Returns null on no match.
        /// </summary>
        public User? SelectByCredentials(string username, string password)
        {
            const string sql = @"
                SELECT id, username, role, image_path
                FROM users
                WHERE username      = @username
                  AND password_hash = SHA2(@password, 256)
                LIMIT 1";

            using var conn   = GetConnection();
            using var cmd    = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new User
            {
                Id        = reader.GetInt32("id"),
                Username  = reader.GetString("username"),
                Role      = reader.GetString("role"),
                ImagePath = reader.IsDBNull(reader.GetOrdinal("image_path"))
                                ? null
                                : reader.GetString("image_path"),
            };
        }

        public void UpdateImagePath(int id, string objectName)
        {
            const string sql = "UPDATE users SET image_path = @imagePath WHERE id = @id";

            using var conn = GetConnection();
            using var cmd  = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id",        id);
            cmd.Parameters.AddWithValue("@imagePath", objectName);
            cmd.ExecuteNonQuery();
        }
    }
}
