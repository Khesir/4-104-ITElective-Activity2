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

        public Transaction? GetById(int id) => _txDatastore.SelectById(id);

        public List<Transaction> GetAll() => _txDatastore.SelectAll();
    }
}
