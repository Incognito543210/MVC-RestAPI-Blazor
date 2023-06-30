namespace API.Helper
{
    public interface IPasswordGetter
    {
        ICollection<string> PopularPasswords();
    }
}