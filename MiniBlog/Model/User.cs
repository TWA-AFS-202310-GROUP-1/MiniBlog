using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace MiniBlog.Model
{
    public class User
    {
        public User()
        {
        }

        public static string CollectionName => "Users";

        public User(string name, string email = "anonymous@unknow.com")
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Email = email;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
