using API.Interfaces;
using DAL;
using Model.MODEL;

namespace API.Services
{
    public class UsersService : IUsersService
    {
        DataContext _context;
        Encryptor _encryptor;

        public UsersService(DataContext context, Encryptor encryptor)
        {
            _context = context;
            _encryptor = encryptor;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CreateUser(User user)
        {
            if (UsernameExists(user.Username))
                return false;

            user.Password = _encryptor.EncryptPassword(user.Password);

            _context.Users.Add(user);

            bool result = Save();

            return result;
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);

            bool result = Save();

            return result;
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.UserID == id);
        }

        public bool UsernameExists(string username)
        {
            return _context.Users.Where(u => u.Username == username).Any();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(c => c.UserID == id).FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.UserID).ToList();
        }
    }
}
