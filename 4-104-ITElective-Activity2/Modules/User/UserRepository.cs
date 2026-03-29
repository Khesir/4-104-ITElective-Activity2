namespace _4_104_ITElective_Activity2.Modules.User
{
    /// <summary>
    /// Domain accessor for users.
    /// Contains no SQL — all persistence is delegated to UserDatastore.
    /// </summary>
    public class UserRepository
    {
        private readonly UserDatastore _datastore;

        public UserRepository(UserDatastore datastore)
        {
            _datastore = datastore;
        }

        public User? FindByCredentials(string username, string password)
            => _datastore.SelectByCredentials(username, password);

        public Task<User?> FindByCredentialsAsync(string username, string password)
            => _datastore.SelectByCredentialsAsync(username, password);

        public List<User> GetAll() => _datastore.SelectAll();

        public Task<List<User>> GetAllAsync() => _datastore.SelectAllAsync();

        public Task<bool> UsernameExistsAsync(string username, int? excludeId = null)
            => _datastore.UsernameExistsAsync(username, excludeId);

        public async Task CreateAsync(User user, string password)
        {
            user.Id = await _datastore.InsertAsync(user, password);
        }

        public Task UpdateFullAsync(User user, string? newPassword)
            => _datastore.UpdateFullAsync(user, newPassword);

        public Task DeleteAsync(int id) => _datastore.DeleteAsync(id);
    }
}
