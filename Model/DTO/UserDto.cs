using System.ComponentModel.DataAnnotations;

namespace Model.DTO
{
    public class UserDto
    {
        [Required(ErrorMessage = "Wpisz nazwę użytkownika.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Wpisz imię.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Wpisz nazwiko.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Wpisz adres e-mail.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres e-mail.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Wpisz hasło.")]
        public string Password { get; set; }
    }
}