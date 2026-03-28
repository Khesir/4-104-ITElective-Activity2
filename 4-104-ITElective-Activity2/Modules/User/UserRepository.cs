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
    }
}
