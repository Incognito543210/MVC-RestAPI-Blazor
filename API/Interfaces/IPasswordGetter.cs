namespace API.Interfaces
{
    public interface IPasswordGetter
    {
        ICollection<string> GetPopularPasswords();
    }
}