using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;
using MiniBlog.Services;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class UserController : ControllerBase
        {
            private readonly ArticleStore articleStore = null!;
            private readonly UserStore userStore = null!;

            public UserController(ArticleStore articleStore, UserStore userStore)
            {
                this.articleStore = articleStore;
                this.userStore = userStore;
            }

            [HttpPost]
            public IActionResult Register(User user)
            {
                if (!userStore.Users.Exists(_ => user.Name.ToLower() == _.Name.ToLower()))
                {
                    userStore.Users.Add(user);
                }

                return CreatedAtAction(nameof(GetByName), new { name = user.Name }, GetByName(user.Name));
            }

            [HttpGet]
            public List<User> GetAll()
            {
                return userStore.Users;
            }

            [HttpPut]
            public User Update(User user)
            {
                var foundUser = userStore.Users.FirstOrDefault(_ => _.Name == user.Name);
                if (foundUser != null)
                {
                    foundUser.Email = user.Email;
                }

                return foundUser;
            }

            [HttpDelete]
            public User Delete(string name)
            {
                var foundUser = userStore.Users.FirstOrDefault(_ => _.Name == name);
                if (foundUser != null)
                {
                    userStore.Users.Remove(foundUser);
                    articleStore.Articles.RemoveAll(a => a.UserName == foundUser.Name);
                }

                return foundUser;
            }

            [HttpGet("{name}")]
            public User GetByName(string name)
            {
                return userStore.Users.FirstOrDefault(_ => _.Name.ToLower() == name.ToLower());
            }
        }    
}

/*using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniBlog.Model;
using MiniBlog.Services;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var registeredUser = await userService.RegisterUser(user);
            return CreatedAtAction(nameof(GetByName), new { name = registeredUser.Name }, registeredUser);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            var updatedUser = await userService.UpdateUser(user);
            if (updatedUser != null)
            {
                return Ok(updatedUser);
            }

            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string name)
        {
            var deletedUser = await userService.DeleteUser(name);
            if (deletedUser != null)
            {
                return Ok(deletedUser);
            }

            return NotFound();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var user = await userService.GetUserByName(name);
            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }
    }
}*/