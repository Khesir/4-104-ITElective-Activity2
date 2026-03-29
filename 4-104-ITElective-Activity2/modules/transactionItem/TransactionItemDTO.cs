using System;
using System.Collections.Generic;
using System.Text;

namespace _4_104_ITElective_Activity2.modules.transactionItem
{
    public class AddedTransactionItemDTO
    {
        public required int     ItemId;
        public required string  Name;
        public required CupSize CupSize;
        public required double  Price;
        public int Quantity = 1;
    }
    public class UpdateTransactionItemDTO
    {
        public required List<TransactionItem> TransactionItem;
    }
}
