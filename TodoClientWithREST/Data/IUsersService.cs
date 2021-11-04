using System.Collections.Generic;
using System.Threading.Tasks;
using TodoClientWithREST.Models;

namespace TodoClientWithREST.Data
{
    public interface IUsersService
    {
        Task<IList<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task RemoveUserAsync(int id);
        Task UpdateUserAsync(User user);
        Task<User> ValidateUser(string username, string password);
    }
}