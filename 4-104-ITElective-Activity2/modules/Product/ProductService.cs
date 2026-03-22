using _4_104_ITElective_Activity2.core;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4_104_ITElective_Activity2.modules.item
{
    public class ProductService
    {
        private readonly ProductRepository _repository;

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
            
            EventBus.Subscribe<AddItemDTO>(HandleAddItem);
            EventBus.Subscribe<LoadItemsRequestDTO>(HandleLoadItems);
        }
        private void HandleLoadItems(LoadItemsRequestDTO dto)
        {
            var items = _repository.GetAll();
            EventBus.Publish(new LoadItemsResultDTO { Items = items });
        }

        public void HandleAddItem(AddItemDTO dto) {
            if (string.IsNullOrEmpty(dto.Name)) return;
            if (dto.Price == 0) return;

            var entity = new Product(dto.Name, dto.Price, dto.ImagePath);
            _repository.Add(entity);

            EventBus.Publish(new AddItemResultDTO { Success = true, Message = "Item added successfully" });
        }
    }
}
