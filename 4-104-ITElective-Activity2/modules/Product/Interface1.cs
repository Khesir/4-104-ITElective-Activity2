using _4_104_ITElective_Activity2.modules.transactionItem;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4_104_ITElective_Activity2.modules.item
{
    // Event bus DTO packet for item module
    public class AddItemDTO
    {
        public required string Name;
        public required double Price;
        public required string ImagePath;
    }
    public class AddItemResultDTO
    {
        public bool Success;
        public string? Message;
    }
    public class LoadItemsRequestDTO { }
    public class LoadItemsResultDTO
    {
        public List<Product> Items = [];
    }
}
