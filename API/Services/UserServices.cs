using API.Interfaces;
using DAL;
using Model.MODEL;

namespace API.Services
{
    public class UserServices : IUserServices
    {
        DataContext _context;
        IUserRepository _userRepository;

        public UserServices(DataContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CreateUser(User user)
        {
            bool result = _userRepository.CreateUser(user);

            result &= Save();

            return result;
        }

        public bool DeleteUser(User user)
        {
            bool result = _userRepository.DeleteUser(user);

            //usuwanie przepisów

            //usuwanie opinii

            result &= Save();

            return result;
        }

        public bool UpdateUser(User user)
        {
            bool result = _userRepository.UpdateUser(user);

            result &= Save();

            return result;
        }

        public bool UserExists(int id)
        {
            return _userRepository.UserExists(id);
        }

        public User GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }
    }
}
