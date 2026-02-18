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
            if (dto.TransactionItem == null) return;
            // Check if there is already a transaction item with the same itemId and cupSize,
            var item = _repository.GetByItemIdAndCupSize(dto.TransactionItem.itemId, dto.TransactionItem.cupSize);
            if (item != null)
            {
                item.quantity += dto.TransactionItem.quantity;
                item.UpdateTotalPrice();
                _repository.Update(item);
                return;
            }
            // if so, just update the quantity and total price
            _repository.Add(dto.TransactionItem);
        }
        private void HandleCreateNewTransaction(CreatedNewTransactionDTO dto)
        {
            _repository.Clear();
        }
    }
}
