using _4_104_ITElective_Activity2.core;
using _4_104_ITElective_Activity2.modules.item;
using _4_104_ITElective_Activity2.modules.transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4_104_ITElective_Activity2.modules.transactionItem
{
    public class TransactionItemService
    {
        private readonly TransactionItemRepository _repository;

        public TransactionItemService(TransactionItemRepository repository)
        {
            _repository = repository;

            EventBus.Subscribe<AddedTransactionItemDTO>(HandleAddTransactionItem);
            EventBus.Subscribe<CreatedNewTransactionDTO>(HandleCreateNewTransaction);
        }

        private void HandleAddTransactionItem(AddedTransactionItemDTO dto)
        {
            var existing = _repository.GetByItemIdAndCupSize(dto.ItemId, dto.CupSize);
            if (existing != null)
            {
                existing.quantity += dto.Quantity;
                existing.UpdateTotalPrice();
                _repository.Update(existing);
                return;
            }

            _repository.Add(new TransactionItem
            {
                itemId   = dto.ItemId,
                name     = dto.Name,
                cupSize  = dto.CupSize,
                price    = dto.Price,
                quantity = dto.Quantity,
            });
        } 
        public void IncreaseQuantity(int id)
        {
            var item = _repository.GetById(id);
            if (item == null) return;
            item.quantity++;
            item.UpdateTotalPrice();
            _repository.Update(item);
        }

        public void DecreaseQuantity(int id)
        {
            var item = _repository.GetById(id);
            if (item == null) return;
            if (item.quantity <= 1) { _repository.Delete(id); return; }
            item.quantity--;
            item.UpdateTotalPrice();
            _repository.Update(item);
        }

        public void RemoveItem(int id) => _repository.Delete(id);

        public void ClearCart() => _repository.Clear();

        private void HandleCreateNewTransaction(CreatedNewTransactionDTO dto)
        {
            _repository.Clear();
        }
    }
}
