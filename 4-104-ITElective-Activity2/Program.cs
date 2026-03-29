using _4_104_ITElective_Activity2.core.Database;
using _4_104_ITElective_Activity2.Core.DI;
using _4_104_ITElective_Activity2.Core.Storage;
using _4_104_ITElective_Activity2.Core.Util;
using _4_104_ITElective_Activity2.Forms;
using _4_104_ITElective_Activity2.modules.item;
using _4_104_ITElective_Activity2.modules.transaction;
using _4_104_ITElective_Activity2.modules.transactionItem;
using _4_104_ITElective_Activity2.Modules.User;
using System.Configuration;

namespace _4_104_ITElective_Activity2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // ── Config ───────────────────────────────────────────────────────
            string connectionString = ConfigurationManager
                .ConnectionStrings["MySqlConnection"]
                .ConnectionString;

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException(
                    "No connection string found. Make sure App.config has MySqlConnection defined.");

            // ── Migrations ───────────────────────────────────────────────────
            new MigrationRunner(connectionString).Run();

            // ── Dependency Injection ─────────────────────────────────────────
            var container = new Container();
            container.Singleton<MinioStore>(() => new MinioStore());

            // Datastores  (singleton — stateless, just hold SQL)
            container.Singleton<ProductDatastore>(        () => new ProductDatastore());
            container.Singleton<UserDatastore>(           () => new UserDatastore());
            container.Singleton<TransactionDatastore>(    () => new TransactionDatastore());
            container.Singleton<TransactionItemDatastore>(() => new TransactionItemDatastore());

            // Repositories  (singleton — delegate to their datastore)
            container.Singleton<ProductRepository>(() =>
                new ProductRepository(container.Resolve<ProductDatastore>()));

            container.Singleton<UserRepository>(() =>
                new UserRepository(container.Resolve<UserDatastore>()));

            container.Singleton<TransactionRepository>(() =>
                new TransactionRepository(
                    container.Resolve<TransactionDatastore>(),
                    container.Resolve<TransactionItemDatastore>()));

            // Cart repository is transient — each cashier session gets a fresh cart
            container.Transient<TransactionItemRepository>(() => new TransactionItemRepository());

            // Services  (singleton — subscribe to EventBus once)
            container.Singleton<ProductService>(() =>
                new ProductService(container.Resolve<ProductRepository>(), container.Resolve<MinioStore>()));

            container.Singleton<TransactionItemService>(() =>
                new TransactionItemService(container.Resolve<TransactionItemRepository>()));
            container.Singleton<TransactionService>(() =>
                new TransactionService(
                    container.Resolve<TransactionRepository>(),
                    container.Resolve<TransactionItemRepository>()));
            container.Singleton<UserService>(() =>
                new UserService(container.Resolve<UserRepository>(), container.Resolve<MinioStore>()));
            container.Singleton<Clock>(() => new Clock());

            ServiceLocator.Configure(container);

            // ── Launch ───────────────────────────────────────────────────────
            ApplicationConfiguration.Initialize();
            ServiceLocator.Get<Clock>().Start();
            ServiceLocator.Get<MinioStore>().EnsureBucketAsync().GetAwaiter().GetResult();
            Application.Run(new Form1());
        }
    }
}
