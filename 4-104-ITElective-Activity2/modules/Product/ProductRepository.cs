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

        public void Update(Product item)
        {
            _datastore.Update(item);
        }

        public void Delete(int id)
        {
            _datastore.Delete(id);
        }

        public Product? GetById(int id) => _datastore.SelectById(id);

        public List<Product> GetAll() => _datastore.SelectAll();
    }
}
