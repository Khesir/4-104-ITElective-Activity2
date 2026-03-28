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
                SELECT id, username, role, image_path,
                       first_name, middle_name, last_name,
                       permanent_address, current_address, contact, gender
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

            return MapUser(reader);
        }

        public List<User> SelectAll()
        {
            const string sql = @"
                SELECT id, username, role, image_path,
                       first_name, middle_name, last_name,
                       permanent_address, current_address, contact, gender
                FROM users ORDER BY id";

            using var conn   = GetConnection();
            using var cmd    = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            var list = new List<User>();
            while (reader.Read())
                list.Add(MapUser(reader));

            return list;
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

        public void UpdateProfile(User user)
        {
            const string sql = @"
                UPDATE users SET
                    first_name        = @firstName,
                    middle_name       = @middleName,
                    last_name         = @lastName,
                    permanent_address = @permanentAddress,
                    current_address   = @currentAddress,
                    contact           = @contact,
                    gender            = @gender
                WHERE id = @id";

            using var conn = GetConnection();
            using var cmd  = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id",               user.Id);
            cmd.Parameters.AddWithValue("@firstName",        (object?)user.FirstName        ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@middleName",       (object?)user.MiddleName       ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@lastName",         (object?)user.LastName         ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@permanentAddress", (object?)user.PermanentAddress ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@currentAddress",   (object?)user.CurrentAddress   ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@contact",          (object?)user.Contact          ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@gender",           (object?)user.Gender           ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        private static User MapUser(MySqlDataReader reader) => new()
        {
            Id               = reader.GetInt32("id"),
            Username         = reader.GetString("username"),
            Role             = reader.GetString("role"),
            ImagePath        = reader.IsDBNull(reader.GetOrdinal("image_path"))        ? null : reader.GetString("image_path"),
            FirstName        = reader.IsDBNull(reader.GetOrdinal("first_name"))        ? null : reader.GetString("first_name"),
            MiddleName       = reader.IsDBNull(reader.GetOrdinal("middle_name"))       ? null : reader.GetString("middle_name"),
            LastName         = reader.IsDBNull(reader.GetOrdinal("last_name"))         ? null : reader.GetString("last_name"),
            PermanentAddress = reader.IsDBNull(reader.GetOrdinal("permanent_address")) ? null : reader.GetString("permanent_address"),
            CurrentAddress   = reader.IsDBNull(reader.GetOrdinal("current_address"))   ? null : reader.GetString("current_address"),
            Contact          = reader.IsDBNull(reader.GetOrdinal("contact"))           ? null : reader.GetString("contact"),
            Gender           = reader.IsDBNull(reader.GetOrdinal("gender"))            ? null : reader.GetString("gender"),
        };
    }
}
