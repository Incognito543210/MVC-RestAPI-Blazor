using Model.MODEL;

namespace API.Interfaces
{
    public interface IUsersService
    {
        bool CreateUser(User user);
        bool DeleteUser(User user);
        User GetUser(int id);
        IEnumerable<User> GetUsers();
        bool Save();
        bool UpdateUser(User user);
        bool UserExists(int id);
    }
}