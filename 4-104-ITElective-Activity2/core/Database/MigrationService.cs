using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace _4_104_ITElective_Activity2.core.Database
{
    public class MigrationService
    {
        private readonly string _connectionString;
        public MigrationService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Run()
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            AcquireLock(conn);
            try
            {
                EnsureMigrationTable(conn);

                var migrationPath = Path.Combine(AppContext.BaseDirectory, "Migrations");

                if (!Directory.Exists(migrationPath))
                {
                    Console.WriteLine($"No Migrations directory found: {migrationPath}");
                    return;
                }
                var files = Directory.GetFiles(migrationPath, "*.sql").OrderBy(f => Path.GetFileName(f));

                if (!files.Any())
                {
                    Console.WriteLine("No migration files found.");
                    return;
                }

                Console.WriteLine($"Found {files.Count()} migration(s). Starting migration process...");
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    Console.WriteLine($"Processing migration: {fileName}");
                    var checksum = GenerateChecksum(file);
                    if (IsApplied(conn, fileName, checksum))
                    {
                        Console.WriteLine($"Skipped: {fileName}");
                        continue;
                    }

                    Console.WriteLine($"Applying: {fileName}");

                    ApplyMigration(conn, file, fileName, checksum);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Migration process failed.");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ReleaseLock(conn);
            }
        }
        private void EnsureMigrationTable(MySqlConnection conn)
        {
            var sql = @"
            CREATE TABLE IF NOT EXISTS migrations (
                id INT AUTO_INCREMENT PRIMARY KEY,
                migration_name VARCHAR(255) NOT NULL UNIQUE,
                checksum VARCHAR(64) NOT NULL,
                applied_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
            );";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
        private bool IsApplied(MySqlConnection conn, string name, string checksum)
        {
            var query = "SELECT checksum FROM migrations WHERE migration_name = @name";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", name);

            var result = cmd.ExecuteScalar();

            // Not found → not applied
            if (result == null)
                return false;

            var dbChecksum = result.ToString();

            // File was modified after being applied
            if (dbChecksum != checksum)
            {
                throw new Exception(
                    $"Migration '{name}' was modified after it was already applied.\n" +
                    $"Database checksum: {dbChecksum}\n" +
                    $"Current file checksum: {checksum}"
                );
            }

            // Already applied and valid
            return true;
        }

        private void ApplyMigration(MySqlConnection conn, string filePath, string name, string checksum)
        {
            var sql = File.ReadAllText(filePath);

            using var tx = conn.BeginTransaction();

            try
            {
                // 🧱 Execute migration SQL
                using (var cmd = new MySqlCommand(sql, conn, tx))
                {
                    cmd.ExecuteNonQuery();
                }

                // 📝 Record migration
                var insertQuery = @"
            INSERT INTO migrations (migration_name, checksum)
            VALUES (@name, @checksum)";

                using (var insertCmd = new MySqlCommand(insertQuery, conn, tx))
                {
                    insertCmd.Parameters.AddWithValue("@name", name);
                    insertCmd.Parameters.AddWithValue("@checksum", checksum);

                    insertCmd.ExecuteNonQuery();
                }

                tx.Commit();

                Console.WriteLine($"Applied: {name}");
            }
            catch (Exception ex)
            {
                tx.Rollback();

                Console.WriteLine($"Failed: {name}");
                Console.WriteLine(ex.Message);

                throw;
            }
        }
        
        private string GenerateChecksum(string filePath)
        {
            using var sha256 = SHA256.Create();

            var bytes = File.ReadAllBytes(filePath);
            var hash = sha256.ComputeHash(bytes);

            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
        private void AcquireLock(MySqlConnection conn)
        {
            using var cmd = new MySqlCommand("SELECT GET_LOCK('migration_lock', 10);", conn);
            var result = cmd.ExecuteScalar();

            if (result == null || Convert.ToInt32(result) != 1)
                throw new Exception("Could not acquire migration lock. Another instance may be running migrations.");
        }

        private void ReleaseLock(MySqlConnection conn)
        {
            using var cmd = new MySqlCommand("SELECT RELEASE_LOCK('migration_lock');", conn);
            cmd.ExecuteScalar();
        }
    }
}
