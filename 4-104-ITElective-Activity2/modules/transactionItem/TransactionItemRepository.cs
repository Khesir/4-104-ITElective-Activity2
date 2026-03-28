using _4_104_ITElective_Activity2.core;

namespace _4_104_ITElective_Activity2.modules.transactionItem
{
    /// <summary>
    /// In-memory cart accessor.
    /// Holds the active transaction items for the current session.
    /// Contains no SQL — persistence is handled by TransactionItemDatastore
    /// when a transaction is finalised.
    /// </summary>
    public class TransactionItemRepository
    {
        private readonly List<TransactionItem> _cache = new();
        private int _nextId = 1;

        public void Add(TransactionItem item)
        {
            item.id = _nextId++;
            _cache.Add(item);
            PublishUpdate();
        }

        public void Update(TransactionItem item)
        {
            var existing = GetById((int)item.id!);
            if (existing == null) return;

            existing.itemId   = item.itemId;
            existing.name     = item.name;
            existing.cupSize  = item.cupSize;
            existing.price    = item.price;
            existing.quantity = item.quantity;
            PublishUpdate();
        }

        public void Delete(int id)
        {
            var existing = GetById(id);
            if (existing == null) return;
            _cache.Remove(existing);
            PublishUpdate();
        }

        public TransactionItem? GetById(int id)
            => _cache.FirstOrDefault(i => i.id == id);

        public TransactionItem? GetByItemIdAndCupSize(int itemId, CupSize cupSize)
            => _cache.FirstOrDefault(i => i.itemId == itemId && i.cupSize == cupSize);

        public List<TransactionItem> GetAll() => _cache.ToList();

        public void Clear()
        {
            _cache.Clear();
            PublishUpdate();
        }

        private void PublishUpdate()
            => EventBus.Publish(new UpdateTransactionItemDTO { TransactionItem = GetAll() });
    }
}
