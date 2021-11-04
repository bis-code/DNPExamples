using System.Threading.Tasks;
using TodoWithLoginFeature.Models;

namespace TodoWithLoginFeature.Authentication
{
    public interface IUserService
    { 
        Task<User> ValidateUser(string username, string password);
    }
}