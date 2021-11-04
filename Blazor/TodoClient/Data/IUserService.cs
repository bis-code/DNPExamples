using System.Threading.Tasks;
using TodoClient.Models;

public interface IUserService
{
    Task<User> ValidateLogin(string username, string password);
}