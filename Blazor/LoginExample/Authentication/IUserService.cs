using LoginExample.Data;

namespace LoginExample.Authentication
{
    public interface IUserService
    {
        User ValidateUser(string username, string password);
    }
}