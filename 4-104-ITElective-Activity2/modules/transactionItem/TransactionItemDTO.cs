using System;
using System.Collections.Generic;
using System.Text;

namespace _4_104_ITElective_Activity2.modules.transactionItem
{
    public class AddedTransactionItemDTO
    {
        public required TransactionItem TransactionItem;
    }
    public class UpdateTransactionItemDTO
    {
        public required List<TransactionItem> TransactionItem;
    }
}
