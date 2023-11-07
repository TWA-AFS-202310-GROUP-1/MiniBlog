using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MiniBlog.Model
{
    public class User
    {
        public User()
        {
        }

        public User(string name, string email = "anonymous@unknow.com")
        {
            this.Name = name;
            this.Email = email;
        }

        public static string UserCollectionName { get; set; } = "users";

        [BsonId]
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
