namespace _4_104_ITElective_Activity2.Modules.User
{
    /// <summary>
    /// Represents the currently logged-in user.
    /// Stored in AppStore as: AppStore.Set(new UserSession(...))
    /// </summary>
    public class UserSession
    {
        public int    Id       { get; }
        public string Username { get; }
        public string Role     { get; }

        public bool IsAdmin    => Role == "admin";
        public bool IsCashier  => Role == "cashier";

        public UserSession(User user)
        {
            Id       = user.Id;
            Username = user.Username;
            Role     = user.Role;
        }
    }
}
