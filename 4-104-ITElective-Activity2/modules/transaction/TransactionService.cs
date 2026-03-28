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
    }
}