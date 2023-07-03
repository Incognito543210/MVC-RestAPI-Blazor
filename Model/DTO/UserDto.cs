using System.ComponentModel.DataAnnotations;

namespace Model.DTO
{
    public class UserDto
    {
        [Required(ErrorMessage = "Wpisz nazwę użytkownika.")]
        [MinLength(6, ErrorMessage = "Nazwa użytkownika powinna składać się z minimum {1} znaków.")]
        [MaxLength(30, ErrorMessage = "Nazwa użytkownika powinna składać się z maksimum {1} znaków.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Wpisz imię.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Wpisz nazwiko.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Wpisz adres e-mail.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres e-mail.")]
        [MinLength(10, ErrorMessage = "Adres e-mail powinien składać się z minimum {1} znaków.")]
        [MaxLength(50, ErrorMessage = "Adres e-mail powinien składać się z maksimum {1} znaków.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Wpisz hasło.")]
        [MinLength(8, ErrorMessage = "Hasło powinno składać się z minimum {1} znaków.")]
        [MaxLength(32, ErrorMessage = "Hasło powinno składać się z maksimum {1} znaków.")]
        public string Password { get; set; }
    }
}