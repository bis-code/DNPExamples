using WebApplication2.Model;

namespace WebApplication2.Data
{
    public interface IUserService
    {
        User ValidateUser(string userName, string password);
    }
}