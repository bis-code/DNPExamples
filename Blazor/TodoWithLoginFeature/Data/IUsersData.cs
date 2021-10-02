using System.Collections.Generic;
using TodoWithLoginFeature.Models;

namespace TodoWithLoginFeature.Data
{
    public interface IUsersData
    {
        IList<User> GetUsers();
        void AddUser(User user);
        void AddTodoToUser(int userID, Todo todo);
        void RemoveUser(int userID);
        void Update(User user);
        User Get(int userID);
    }
}