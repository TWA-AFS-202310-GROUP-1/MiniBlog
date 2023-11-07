using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.Services
{
    public class UserService
    {
/*        private readonly UserStore userStore;
        private readonly ArticleStore articleStore;*/
        private readonly IUserRepository userRepository = null!;

        public UserService(UserStore userStore, ArticleStore articleStore, IUserRepository userRepository)
        {
/*            this.userStore = userStore;
            this.articleStore = articleStore;*/
            this.userRepository = userRepository;
        }

        public async Task<User> RegisterUser(User user)
        {
            var existingUser = await userRepository.GetByName(user.Name);
            if (existingUser == null)
            {
                return await userRepository.CreateUser(user);
            }

            return existingUser;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await userRepository.GetUsers();
        }

        public async Task<User> UpdateUser(User user)
        {
            return await userRepository.UpdateUser(user);
        }

        public async Task<User> DeleteUser(string name)
        {
            return await userRepository.DeleteUser(name);
        }

        public async Task<User> GetUserByName(string name)
        {
            return await userRepository.GetByName(name);
        }
    }
}
