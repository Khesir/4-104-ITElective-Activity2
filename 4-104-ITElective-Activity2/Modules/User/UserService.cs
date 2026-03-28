using _4_104_ITElective_Activity2.core;

using System;
using System.Collections.Generic;
using System.Text;

namespace _4_104_ITElective_Activity2.Modules.User
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
            
            //EventBus.Subscribe<AddUserDTO>(HandleAddUser);
            //EventBus.Subscribe<LoadUsersRequestDTO>(HandleLoadUsers);
        }
        public List<User> GetAllUsers()
        {
            return _repository.GetAll();
        }
    }
}
