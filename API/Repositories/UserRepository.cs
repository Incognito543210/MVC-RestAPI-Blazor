using API.Interfaces;
using DAL;
using Model.MODEL;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
      
        public User GetUser(int id)
        {
            return _context.Users.Where(c => c.UserID == id).FirstOrDefault();
        }
   
        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.UserID).ToList();
        }

        public bool UsernameExists(string username)
        {
            return _context.Users.Where(u=>u.Username == username).Any();
        }
        public bool UserExists(int id)
        {
            return _context.Users.Any(u=>u.UserID == id);
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return true;
        }

        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return true;
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            return true;
        }
    }
}
