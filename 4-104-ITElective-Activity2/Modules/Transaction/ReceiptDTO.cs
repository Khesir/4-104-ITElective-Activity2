using System;
using System.Collections.Generic;
using System.Text;

namespace _4_104_ITElective_Activity2.Modules.Transaction
{
    public class ReceiptDTO
    {
        public required string Name { get; set; }
        public required string CupSize { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
    }
}
