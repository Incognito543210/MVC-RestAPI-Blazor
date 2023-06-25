using Model.MODEL;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        public bool UserExists(int id);
        public User GetUser(int id);
        bool DeleteUser(User user);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool UsernameExists(string username);
        ICollection<User> GetUsers();
    }
}
