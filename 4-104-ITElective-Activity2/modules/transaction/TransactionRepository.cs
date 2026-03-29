using _4_104_ITElective_Activity2.modules.transactionItem;

namespace _4_104_ITElective_Activity2.modules.transaction
{
    /// <summary>
    /// Domain accessor for transactions.
    /// Contains no SQL — all persistence is delegated to the Datastores.
    /// </summary>
    public class TransactionRepository
    {
        private readonly TransactionDatastore     _txDatastore;
        private readonly TransactionItemDatastore _itemDatastore;

        public TransactionRepository(
            TransactionDatastore     txDatastore,
            TransactionItemDatastore itemDatastore)
        {
            _txDatastore   = txDatastore;
            _itemDatastore = itemDatastore;
        }

        /// <summary>
        /// Persists a completed transaction and all its items.
        /// Returns the new transaction id.
        /// </summary>
        public int Save(Transaction transaction)
        {
            int id = _txDatastore.Insert(transaction);
            _itemDatastore.InsertAll(id, transaction.items);
            return id;
        }

        public async Task<int> SaveAsync(Transaction transaction)
        {
            int id = await _txDatastore.InsertAsync(transaction);
            await _itemDatastore.InsertAllAsync(id, transaction.items);
            return id;
        }

        public Transaction? GetById(int id) => _txDatastore.SelectById(id);

        public Task<Transaction?> GetByIdAsync(int id) => _txDatastore.SelectByIdAsync(id);

        public List<Transaction> GetAll() => _txDatastore.SelectAll();

        public Task<List<Transaction>> GetAllAsync() => _txDatastore.SelectAllAsync();

        public int GetTransactionCountByUser(int userId) => _txDatastore.CountByUserId(userId);

        public Task<int> GetTransactionCountByUserAsync(int userId) => _txDatastore.CountByUserIdAsync(userId);

        public Task<List<TransactionItem>> GetItemsByTransactionIdAsync(int transactionId)
            => _itemDatastore.SelectByTransactionIdAsync(transactionId);
    }
}
