using System;
using System.Collections.Generic;
using System.Text;

namespace _4_104_ITElective_Activity2.core.Database
{
    public class MigrationRunner
    {
        private readonly MigrationService _migrationService;

        public MigrationRunner(string connectionString)
        {
            _migrationService = new MigrationService(connectionString);
        }

        public void Run()
        {
            Console.WriteLine("=================================");
            Console.WriteLine("   DATABASE MIGRATION RUNNER     ");
            Console.WriteLine("=================================");

            try
            {
                _migrationService.Run();

                Console.WriteLine("Migration process completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Migration process failed.");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("=================================");
        }
    }
}
