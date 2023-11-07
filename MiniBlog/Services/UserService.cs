using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Services
{
    public class UserService
    {
        private readonly ArticleStore articleStore = null!;
        private readonly UserStore userStore = null!;
        private readonly IUserRepository userRepository = null!;

        public UserService(ArticleStore articleStore, UserStore userStore, IUserRepository userRepository)
        {
            this.articleStore = articleStore;
            this.userStore = userStore;
            this.userRepository = userRepository;
        }

        public async Task<List<User>> GetAll()
        {
            return await userRepository.GetUsers();
        }

        public async Task<User> GetByName(string name)
        {
            return await userRepository.GetUserByName(name);
        }

        public async Task<User?> Register(User user)
        {
            return await userRepository.Register(user);
        }
    }
}
