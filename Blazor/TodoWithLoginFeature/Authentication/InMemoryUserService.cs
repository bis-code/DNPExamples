using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TodoWithLoginFeature.Models;

namespace TodoWithLoginFeature.Authentication
{
    public class InMemoryUserService : IUserService
    {
        private List<User> users;
        private string usersFile = "users.json";

        public InMemoryUserService()
        {
            if (!File.Exists(usersFile))
            {
                users = users = new[]
                {
                    new User()
                    {
                        Username = "Ionut",
                        UserID = 1,
                        Password = "12345",
                        City = "Ploiesti",
                        SecurityLevel = 4,
                        Role = "Admin",
                        Birthday = 2001
                    },
                    new User()
                    {
                        Username = "Baicoianu",
                        UserID = 2,
                        Password = "12345",
                        City = "Horsens",
                        SecurityLevel = 2,
                        Role = "Member",
                        Birthday = 2005
                    }
                }.ToList();
                string userAsJson = JsonSerializer.Serialize(users);
                File.WriteAllText(usersFile, userAsJson);
            }
            else
            {
                string content = File.ReadAllText(usersFile);
                users = JsonSerializer.Deserialize<List<User>>(content);
            }
        }

        public User ValidateUser(string username, string password)
        {
            User first = users.FirstOrDefault(user => user.Username.Equals(username));
            
            if (first == null) throw new Exception("User not found");
            if (!first.Password.Equals(password)) throw new Exception("Incorrect password");

            return first;
        }
    }
}