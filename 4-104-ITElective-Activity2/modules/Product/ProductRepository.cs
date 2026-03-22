using System;
using System.Collections.Generic;
using System.Text;

namespace _4_104_ITElective_Activity2.modules.item
{
    public class ProductRepository
    {
        private readonly List<Product> _cache = new();
        private int _nextId = 1;
        public ProductRepository()
        {
            // Initialize with some dummy data
            Add(new Product("Creamy Pure Match", 190, "matcha_latte.jpeg"));
            Add(new Product("Straberry Cream", 150, "strawberries_cream.jpg"));
            Add(new Product("Dragon Drink", 210, "dragon_drink.jpeg"));
        }

        public void Add(Product item)
        {
            item.id = _nextId++;
            _cache.Add(item);
        }
        public void Update(Product item)
        {
            var existingItem = GetById((int)item.id!);
            if (existingItem == null) return;

            existingItem.name = item.name;
            existingItem.price = item.price;
            existingItem.imagePath = item.imagePath;
        }
        public void Delete(int id) {

            var existingItem = GetById(id);
            if (existingItem == null) return;
            
            _cache.Remove(existingItem);
        }
        public Product? GetById(int id) { 
            return _cache.FirstOrDefault(i => i.id == id);
        }
        public List<Product> GetAll()
        {
            return _cache.ToList(); // Just return a copy not the real list address lmao
        }
    }
}
