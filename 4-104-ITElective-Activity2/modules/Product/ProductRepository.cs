namespace _4_104_ITElective_Activity2.modules.item
{
    /// <summary>
    /// Domain accessor for products.
    /// Contains no SQL — all persistence is delegated to ProductDatastore.
    /// </summary>
    public class ProductRepository
    {
        private readonly ProductDatastore _datastore;

        public ProductRepository(ProductDatastore datastore)
        {
            _datastore = datastore;
        }

        public void Add(Product item)
        {
            item.id = _datastore.Insert(item);
        }

        public async Task AddAsync(Product item)
        {
            item.id = await _datastore.InsertAsync(item);
        }

        public void Update(Product item) => _datastore.Update(item);

        public Task UpdateAsync(Product item) => _datastore.UpdateAsync(item);

        public void Delete(int id) => _datastore.Delete(id);

        public Task DeleteAsync(int id) => _datastore.DeleteAsync(id);

        public Product? GetById(int id) => _datastore.SelectById(id);

        public Task<Product?> GetByIdAsync(int id) => _datastore.SelectByIdAsync(id);

        public List<Product> GetAll() => _datastore.SelectAll();

        public Task<List<Product>> GetAllAsync() => _datastore.SelectAllAsync();
    }
}
