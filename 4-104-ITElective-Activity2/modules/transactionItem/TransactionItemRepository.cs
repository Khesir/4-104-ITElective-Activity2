using _4_104_ITElective_Activity2.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4_104_ITElective_Activity2.modules.transactionItem
{
    public class TransactionItemRepository
    {
        private readonly List<TransactionItem> _cache = new();
        private int _nextId = 1;

        public TransactionItemRepository() { }

        public void Add(TransactionItem item)
        {
            item.id = _nextId++;
            _cache.Add(item);
            EventBus.Publish(new UpdateTransactionItemDTO
            {
                TransactionItem = GetAll(),
            });
        }
        public void Update(TransactionItem item)
        {
            var existingItem = GetById((int)item.id!);
            if (existingItem == null) return;
            existingItem.itemId = item.itemId;
            existingItem.name = item.name;
            existingItem.cupSize = item.cupSize;
            existingItem.price = item.price;
            existingItem.quantity = item.quantity;
            EventBus.Publish(new UpdateTransactionItemDTO
            {
                TransactionItem = GetAll(),
            });
        }
        public void Delete(int id)
        {
            var existingItem = GetById(id);
            if (existingItem == null) return;
            _cache.Remove(existingItem);
            EventBus.Publish(new UpdateTransactionItemDTO
            {
                TransactionItem = GetAll(),
            });
        }
        public TransactionItem? GetById(int id)
        {
            return _cache.FirstOrDefault(i => i.id == id);
        }
        public TransactionItem? GetByItemIdAndCupSize(int itemId, CupSize cupSize)
        {
            return _cache.FirstOrDefault(i => i.itemId == itemId && i.cupSize == cupSize);
        }
        public List<TransactionItem> GetAll()
        {
            return _cache.ToList();
        }
        public void Clear()
        {
            _cache.Clear();
            EventBus.Publish(new UpdateTransactionItemDTO
            {
                TransactionItem = GetAll(),
            });
        }
    }
}
