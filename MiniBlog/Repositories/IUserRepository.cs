using MiniBlog.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(string userName);
        Task<User> GetByName(string name);
    }
}
