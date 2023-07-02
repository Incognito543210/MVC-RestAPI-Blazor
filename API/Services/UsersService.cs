using API.Interfaces;
using DAL;
using Model.MODEL;
using System.Text.RegularExpressions;

namespace API.Services
{
    public class UsersService : IUsersService
    {
        DataContext _context;
        IEncryptor _encryptor;
        IPasswordGetter _passwordGetter;

        public UsersService(DataContext context, IEncryptor encryptor, IPasswordGetter passwordGetter)
        {
            _context = context;
            _encryptor = encryptor;
            _passwordGetter = passwordGetter;
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

        public bool EmailExists(string email)
        {
            return _context.Users.Where(u=>u.Email == email).Any();
        }

        public User Logger(string login, string password)
        {
            string pass = _encryptor.EncryptPassword(password);
            if (UsernameExists(login))
            {
                return _context.Users.Where(u =>u.Username == login).Where(p=>p.Password==pass).FirstOrDefault();
            }
            else
            {
                return _context.Users.Where(u => u.Email == login).Where(p => p.Password == pass).FirstOrDefault();
            }
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(c => c.UserID == id).FirstOrDefault();
        }
        public IEnumerable<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.UserID).ToList();
        }

        public bool IsEmailValid(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }

        public bool IsPasswordStrong(string password)
        {
            //Musi zawierać co najmniej 8 liter
            //Musi mieć co najmniej jedną dużą literę
            //Musi mieć co najmniej jedną małą literę
            //Musi mieć co najmniej jedną cyfrę
            //Musi mieć co najmniej jeden znak specjalny
            Regex regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            Match match = regex.Match(password);
            if (match.Success)
                return true;
            else
                return false;
        }

        public bool IsPasswordPopular(string password)
        {
            ICollection<string> popularPasswords = _passwordGetter.GetPopularPasswords();

            foreach (string popularPassword in popularPasswords)
                if (popularPassword == password)
                    return true;

            return false;
        }
    }
}
