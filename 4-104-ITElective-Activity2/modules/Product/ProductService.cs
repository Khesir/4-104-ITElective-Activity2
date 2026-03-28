using _4_104_ITElective_Activity2.core;
using _4_104_ITElective_Activity2.Core.Storage;

namespace _4_104_ITElective_Activity2.modules.item
{
    public class ProductService
    {
        private readonly ProductRepository _repository;
        private readonly MinioStore        _minioStore;

        public ProductService(ProductRepository repository, MinioStore minioStore)
        {
            _repository = repository;
            _minioStore = minioStore;

            EventBus.Subscribe<AddItemDTO>(HandleAddItem);
            EventBus.Subscribe<UpdateItemDTO>(HandleUpdateItem);
            EventBus.Subscribe<LoadItemsRequestDTO>(HandleLoadItems);
        }

        private void HandleLoadItems(LoadItemsRequestDTO dto)
        {
            var items = _repository.GetAll();
            EventBus.Publish(new LoadItemsResultDTO { Items = items });
        }

        public void HandleAddItem(AddItemDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Name)) return;
            if (dto.Price == 0) return;

            string imagePath = string.Empty;

            if (dto.ImageStream != null && dto.ImageFileName != null && dto.ImageContentType != null)
                imagePath = _minioStore.UploadAsync(dto.ImageStream, dto.ImageFileName, dto.ImageContentType)
                                       .GetAwaiter().GetResult();

            var entity = new Product(dto.Name, dto.Price, imagePath);
            _repository.Add(entity);

            EventBus.Publish(new AddItemResultDTO { Success = true, Message = "Item added successfully" });
        }

        public void HandleUpdateItem(UpdateItemDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Name)) return;

            var existing = _repository.GetById(dto.Id);
            if (existing == null) return;

            string imagePath = existing.imagePath;

            if (dto.ImageStream != null && dto.ImageFileName != null && dto.ImageContentType != null)
            {
                // Delete old image from MinIO if it exists
                if (!string.IsNullOrEmpty(existing.imagePath))
                    _minioStore.DeleteAsync(existing.imagePath).GetAwaiter().GetResult();

                imagePath = _minioStore.UploadAsync(dto.ImageStream, dto.ImageFileName, dto.ImageContentType)
                                       .GetAwaiter().GetResult();
            }

            existing.name      = dto.Name;
            existing.price     = dto.Price;
            existing.imagePath = imagePath;

            _repository.Update(existing);

            EventBus.Publish(new AddItemResultDTO { Success = true, Message = "Item updated successfully" });
        }
    }
}
