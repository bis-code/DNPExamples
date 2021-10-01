using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TodoWithLogin.Models;

namespace TodoWithLogin.Authentication
{
    public class InMemoryUserService : IUserService
    {
        private List<User> users;
        private string usersFile = "users.json";

        public InMemoryUserService()
        {
            string content = File.ReadAllText(usersFile);
            users = JsonSerializer.Deserialize<List<User>>(content);
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