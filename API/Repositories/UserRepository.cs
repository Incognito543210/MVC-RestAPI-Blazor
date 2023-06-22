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

        public bool UserExists(int id)
        {
            return _context.Users.Any(u=>u.UserID == id);
        }
    }
}
