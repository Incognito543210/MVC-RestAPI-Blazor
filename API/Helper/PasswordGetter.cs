namespace API.Helper
{
    public class PasswordGetter : IPasswordGetter
    {
        private readonly string popularPasswordsStringPath = "10-million-password-list-top-1000000.txt";

        public ICollection<string> PopularPasswords()
        {
            if (!File.Exists(popularPasswordsStringPath))
                throw new FileNotFoundException("Nie znaleziono pliku z popularnymi hasłami");
            ICollection<string> result = new List<string>(File.ReadAllLines(popularPasswordsStringPath));
            return result;
        }
    }
}
