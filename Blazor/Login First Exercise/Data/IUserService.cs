using Login_First_Exercise.Model;

namespace Login_First_Exercise.Data
{
    public interface IUserService
    {
        User ValidateUser(string userName, string password);
    }
}