using Model.DTO;
using Model.MODEL;

namespace API.Interfaces
{
    public interface IUsersService
    {
        bool CreateUser(User user);
        bool EmailExists(string email);
        User GetUser(int id);
        IEnumerable<User> GetUsers();
        bool IsEmailValid(string email);
        bool IsPasswordPopular(string password);
        bool IsPasswordStrong(string password);
        int Logger(string login, string password);
        bool Save();
        bool UpdateUser(User user);
        bool UserExists(int id);
        bool UsernameExists(string username);
        SessionDto StartSession(int id);
        bool DoesPasswordHasSpecialCharacters(string password);
    }
}