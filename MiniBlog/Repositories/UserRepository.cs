using MiniBlog.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> userCollection;

        public UserRepository(IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase("MiniBlog");
            userCollection = mongoDatabase.GetCollection<User>(User.CollectionName);
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await userCollection.Find(_ => true).ToListAsync();
            return users;
        }

        public async Task<User> CreateUser(User user)
        {
            await userCollection.InsertOneAsync(user);
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            var update = Builders<User>.Update.Set(u => u.Email, user.Email);
            var result = await userCollection.FindOneAndUpdateAsync(u => u.Name == user.Name, update);
            return result;
        }

        public async Task<User> DeleteUser(string userName)
        {
            var deletedUser = await userCollection.FindOneAndDeleteAsync(u => u.Name == userName);
            return deletedUser;
        }

        public async Task<User> GetByName(string name)
        {
            var user = await userCollection.Find(u => u.Name == name).FirstOrDefaultAsync();
            return user;
        }
    }
}
