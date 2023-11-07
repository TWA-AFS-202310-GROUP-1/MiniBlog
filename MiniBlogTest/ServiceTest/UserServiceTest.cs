using MiniBlog;
using MiniBlog.Model;
using MiniBlog.Repositories;
using MiniBlog.Stores;
using Newtonsoft.Json;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MiniBlogTest.ServiceTest
{
    public class UserServiceTest : TestBase
    {
        public UserServiceTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async void Should_get_all_users()
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(repository => repository.GetUsers()).Returns(Task.FromResult(new List<User>()));
            var client = GetClient(new ArticleStore(), new UserStore(new List<User>()), null, mock.Object);
            var response = await client.GetAsync("/user");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(body);
            Assert.Equal(0, users.Count);
        }

        [Fact]
        public async void Should_get_user_by_name()
        {
            var mock = new Mock<IUserRepository>();
            string name = "Tom";
            mock.Setup(repository => repository.GetUserByName(name)).Returns(Task.FromResult(new User("Tom", "anonymous@unknow.com")));
            var client = GetClient(new ArticleStore(), new UserStore(new List<User>()), null, mock.Object);
            var response = await client.GetAsync($"/user/{name}");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(body);
            Assert.Equal("Tom", user.Name);
        }
    }
}
