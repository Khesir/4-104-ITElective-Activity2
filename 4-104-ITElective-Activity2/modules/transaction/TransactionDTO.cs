using System;
using System.Collections.Generic;
using System.Text;

namespace _4_104_ITElective_Activity2.modules.transaction
{
    public class CreatedNewTransactionDTO {}

    public class SaveTransactionDTO
    {
        public int? UserId;
        public List<transactionItem.TransactionItem> Items = new();
    }
}
