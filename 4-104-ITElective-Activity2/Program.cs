using _4_104_ITElective_Activity2.core.Database;
using _4_104_ITElective_Activity2.forms;
using System.Configuration;

namespace _4_104_ITElective_Activity2
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ── Config ──────────────────────────────────────────
            string connectionString = ConfigurationManager
                .ConnectionStrings["MySqlConnection"]
                .ConnectionString;
            Console.WriteLine($"Connection String: {connectionString}");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException(
                    "No connection string found. Make sure App.config has MySqlConnection defined."
                );

            // ── Migrations ──────────────────────────────────────
            new MigrationRunner(connectionString).Run();


            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}