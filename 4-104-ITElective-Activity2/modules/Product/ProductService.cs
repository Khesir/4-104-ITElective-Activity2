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
        }

        public List<Product> GetAllProducts() => _repository.GetAll();

        public Task<List<Product>> GetAllProductsAsync() => _repository.GetAllAsync();

        public Task DeleteProductAsync(int id) => _repository.DeleteAsync(id);

        public Task<string?> GetProductImageUrlAsync(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath)) return Task.FromResult<string?>(null);
            return _minioStore.GetUrlAsync(imagePath)!;
        }

        public async Task AddProductAsync(AddItemDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Name)) return;
            if (dto.Price == 0) return;

            string imagePath = string.Empty;

            if (dto.ImageStream != null && dto.ImageFileName != null && dto.ImageContentType != null)
                imagePath = await _minioStore.UploadAsync(dto.ImageStream, dto.ImageFileName, dto.ImageContentType);

            var entity = new Product(dto.Name, dto.Price, imagePath) { isAvailable = dto.IsAvailable };
            _repository.Add(entity);
        }

        public async Task UpdateProductAsync(UpdateItemDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Name)) return;

            var existing = _repository.GetById(dto.Id);
            if (existing == null) return;

            string imagePath = existing.imagePath;

            if (dto.ImageStream != null && dto.ImageFileName != null && dto.ImageContentType != null)
            {
                if (!string.IsNullOrEmpty(existing.imagePath))
                    await _minioStore.DeleteAsync(existing.imagePath);

                imagePath = await _minioStore.UploadAsync(dto.ImageStream, dto.ImageFileName, dto.ImageContentType);
            }

            existing.name        = dto.Name;
            existing.price       = dto.Price;
            existing.imagePath   = imagePath;
            existing.isAvailable = dto.IsAvailable;

            _repository.Update(existing);
        }
    }
}
