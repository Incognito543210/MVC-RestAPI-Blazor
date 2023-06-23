using Model.MODEL;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        public bool UserExists(int id);
        public User GetUser(int id);

 

    }
}
