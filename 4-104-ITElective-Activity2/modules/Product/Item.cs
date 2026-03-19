using System;
using System.Collections.Generic;
using System.Text;

namespace _4_104_ITElective_Activity2.modules.item
{
    public class Item
    {
        public int? id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string imagePath { get; set; }
        public Item(string name, double price, string imagePath)
        {
            this.name = name;
            this.price = price;
            this.imagePath = imagePath;
        }
    }
}
