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
        // Image upload — all three must be set together, or all null (no image)
        public Stream?  ImageStream;
        public string?  ImageFileName;
        public string?  ImageContentType;
    }
    public class UpdateItemDTO
    {
        public required int    Id;
        public required string Name;
        public required double Price;
        // Leave null to keep the existing image unchanged
        public Stream?  ImageStream;
        public string?  ImageFileName;
        public string?  ImageContentType;
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
