using TodoWithLogin.Models;

namespace TodoWithLogin.Authentication
{
    public interface IUserService
    {
        User ValidateUser(string username, string password);
    }
}