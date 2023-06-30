using Model.MODEL;

namespace API.Interfaces
{
    public interface IUsersService
    {
        bool CreateUser(User user);
        User GetUser(int id);
        IEnumerable<User> GetUsers();
        bool IsEmailValid(string email);
        bool Save();
        bool UpdateUser(User user);
        bool UserExists(int id);
    }
}