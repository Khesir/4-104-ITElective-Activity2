using _4_104_ITElective_Activity2.core;
using _4_104_ITElective_Activity2.Core.Storage;

using System;
using System.Collections.Generic;
using System.Text;

namespace _4_104_ITElective_Activity2.Modules.User
{
    public class UserService
    {
        private readonly UserRepository _repository;
        private readonly MinioStore _minioStore;

        public UserService(UserRepository repository, MinioStore minioStore)
        {
            _repository = repository;
            _minioStore = minioStore;

            //EventBus.Subscribe<AddUserDTO>(HandleAddUser);
            //EventBus.Subscribe<LoadUsersRequestDTO>(HandleLoadUsers);
        }
        public List<User> GetAllUsers() => _repository.GetAll();

        public Task<List<User>> GetAllUsersAsync() => _repository.GetAllAsync();

        public Task<bool> UsernameExistsAsync(string username, int? excludeId = null)
            => _repository.UsernameExistsAsync(username, excludeId);

        public async Task CreateUserAsync(User user, string password,
            Stream? imageStream, string? imageFileName, string? imageContentType)
        {
            if (imageStream != null && imageFileName != null && imageContentType != null)
                user.ImagePath = await _minioStore.UploadAsync(imageStream, imageFileName, imageContentType);

            await _repository.CreateAsync(user, password);
        }

        public async Task UpdateUserAsync(User user, string? newPassword,
            Stream? imageStream, string? imageFileName, string? imageContentType)
        {
            if (imageStream != null && imageFileName != null && imageContentType != null)
            {
                if (!string.IsNullOrEmpty(user.ImagePath))
                    await _minioStore.DeleteAsync(user.ImagePath);

                user.ImagePath = await _minioStore.UploadAsync(imageStream, imageFileName, imageContentType);
            }

            await _repository.UpdateFullAsync(user, newPassword);
        }

        public Task DeleteUserAsync(int id) => _repository.DeleteAsync(id);

        public Task<string?> GetProfileImageUrlAsync(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath)) return Task.FromResult<string?>(null);
            return _minioStore.GetUrlAsync(imagePath)!;
        }
    }
}
