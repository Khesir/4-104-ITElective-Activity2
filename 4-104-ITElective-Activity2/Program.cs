using _4_104_ITElective_Activity2.core.Database;
using _4_104_ITElective_Activity2.forms;
using Microsoft.Extensions.Configuration;

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
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Local.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            Console.Write($"Connection String: {connectionString}\n");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException(
                    "No connection string found. Add appsettings.Local.json or set environment variables."
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