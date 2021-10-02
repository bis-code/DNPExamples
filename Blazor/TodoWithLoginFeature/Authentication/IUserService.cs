using TodoWithLoginFeature.Models;

namespace TodoWithLoginFeature.Authentication
{
    public interface IUserService
    { 
        User ValidateUser(string username, string password);
    }
}