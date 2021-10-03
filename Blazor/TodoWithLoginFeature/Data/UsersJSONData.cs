using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TodoWithLoginFeature.Models;

namespace TodoWithLoginFeature.Data
{
    public class UsersJSONData : IUsersData
    {
        private IList<User> users;
        private string usersFile = "users.json";

        public UsersJSONData()
        {
            if (!File.Exists(usersFile))
            {
                Seed();
                WriteUsersToFile();
            }
            else
            {
                string content = File.ReadAllText(usersFile);
                users = JsonSerializer.Deserialize<List<User>>(content);
            }
        }
        private void Seed()
        {
            users = new[]
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
        }

        public IList<User> GetUsers()
        {
            List<User> tmp = new List<User>(users);
            return tmp;
        }

        public void AddUser(User user)
        {
            int max = users.Max(user => user.UserID);
            user.UserID = (++max);
            user.Todos = new List<Todo>();
            user.Role = "Member";
            user.SecurityLevel = 0;
            users.Add(user);
            WriteUsersToFile();
        }

        public void RemoveUser(int userID)
        {
            User toRemove = users.First(u => u.UserID == userID);
            users.Remove(toRemove);
            WriteUsersToFile();
        }

        public void Update(User user)
        {
            //to be updated
            User toUpdate = users.First(u => u.UserID == user.UserID);
            toUpdate.Username = user.Username;
            toUpdate.Role = user.Role;
            toUpdate.SecurityLevel = user.SecurityLevel;
            toUpdate.Password = user.Password;
            WriteUsersToFile();
        }

        public User Get(int userID)
        {
            return users.FirstOrDefault(u => u.UserID == userID);
        }

        public void AddTodoToUser(int userID, Todo todo)
        {
            User toUpdate = users.First(u => u.UserID == userID);
            toUpdate.addTodo(todo);
            WriteUsersToFile();
        }
        private void WriteUsersToFile()
        {
            string userAsJson = JsonSerializer.Serialize(users);
            File.WriteAllText(usersFile, userAsJson);
        }
    }
}