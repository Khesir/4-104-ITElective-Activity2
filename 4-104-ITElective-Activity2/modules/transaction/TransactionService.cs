using _4_104_ITElective_Activity2.modules.transactionItem;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4_104_ITElective_Activity2.modules.transaction
{
    public class TransactionService
    {
        private readonly TransactionRepository _repository;
        private readonly TransactionItemRepository _transactionItemRepository;
        public TransactionService(TransactionRepository repository, TransactionItemRepository itemRepository)
        {
            _repository = repository;
            _transactionItemRepository = itemRepository;

            //EventBus.Subscribe<AddTransactionDTO>(HandleAddTransaction);
            //EventBus.Subscribe<LoadTransactionsRequestDTO>(HandleLoadTransactions);
        }
        public List<Transaction> GetAllTransactions() => _repository.GetAll();

        public Task<List<Transaction>> GetAllTransactionsAsync() => _repository.GetAllAsync();

        public int GetTransactionCountByUser(int userId) => _repository.GetTransactionCountByUser(userId);

        public Task<int> GetTransactionCountByUserAsync(int userId) => _repository.GetTransactionCountByUserAsync(userId);

        public Task<List<TransactionItem>> GetItemsByTransactionIdAsync(int transactionId)
            => _repository.GetItemsByTransactionIdAsync(transactionId);

        public Task<int> SaveTransactionAsync(SaveTransactionDTO dto)
        {
            var transaction = new Transaction
            {
                userId    = dto.UserId,
                createdAt = DateTime.Now,
            };

            foreach (var item in dto.Items)
                transaction.AddItem(item);

            return _repository.SaveAsync(transaction);
        }
    }
}