namespace _4_104_ITElective_Activity2.Modules.User
{
    public class CreateUserDTO
    {
        public required string Username;
        public required string Password;
        public required string Role;
        public string? FirstName;
        public string? MiddleName;
        public string? LastName;
        public string? Contact;
        public string? PermanentAddress;
        public string? CurrentAddress;
        public string? Gender;
        public Stream?  ImageStream;
        public string?  ImageFileName;
        public string?  ImageContentType;
    }

    public class UpdateUserDTO
    {
        public required int    Id;
        public required string Username;
        public required string Role;
        public string? FirstName;
        public string? MiddleName;
        public string? LastName;
        public string? Contact;
        public string? PermanentAddress;
        public string? CurrentAddress;
        public string? Gender;
        public string? NewPassword;
        public string? ExistingImagePath;
        public Stream?  ImageStream;
        public string?  ImageFileName;
        public string?  ImageContentType;
    }
}
