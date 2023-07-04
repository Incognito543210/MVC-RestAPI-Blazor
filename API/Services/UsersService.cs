using API.Interfaces;
using DAL;
using Microsoft.IdentityModel.Tokens;
using Model.DTO;
using Model.MODEL;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
            return _context.Users.Where(u => u.Email == email).Any();
        }

        public SessionDto Logger(string login, string password)
        {
            string pass = _encryptor.EncryptPassword(password);
            User user = new User();

            if (UsernameExists(login))
            {
                 user = _context.Users.Where(u => u.Username == login).Where(p => p.Password == pass).FirstOrDefault();
            }
            else
            {
                user =  _context.Users.Where(u => u.Email == login).Where(p => p.Password == pass).FirstOrDefault();
            }
            SessionDto session = StartSession(user.UserID);
            return session;
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

        public SessionDto StartSession(int id)
        {
            var session = new SessionDto();

            session.AccessToken = GenerateBearerToken(id);
            session.RefreshToken = GenerateRefreshToken(id);

            return session;
        }

        private string GenerateBearerToken(int id)
        {
            DateTimeOffset expiry = DateTimeOffset.Now.AddHours(1);
            IEnumerable<Claim> userClaims = GetClaimsForUser(GetUser(id));
            string token = CreateToken(expiry, userClaims);

            return token;
        }

        private string GenerateRefreshToken(int id)
        {
            DateTimeOffset expiry = DateTimeOffset.Now.AddDays(30);
            IEnumerable<Claim> userClaims = GetClaimsForUser(GetUser(id));
            string token = CreateToken(expiry, userClaims);

            return token;
        }

        private string CreateToken(DateTimeOffset expiryDate, IEnumerable<Claim> claims)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SecretKeySecretKey"));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: "Przepisy.Api",
                audience: "Przepisy.View",
                claims: claims,
                notBefore: DateTime.Now,
                expires: expiryDate.DateTime,
                signingCredentials: credentials);
            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        private IEnumerable<Claim> GetClaimsForUser(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.GivenName, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim("WWWServiceID", user.UserID.ToString()),
                new Claim("LastSynced", DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd HH:mm:ss")),
            };

            return claims;
        }
    }
}
