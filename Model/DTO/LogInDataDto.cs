using System.ComponentModel.DataAnnotations;

namespace Model.DTO
{
    public class LogInDataDto
    {
        [Required(ErrorMessage = "Wpisz nazwę użytkownika lub adres e-mail.")]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "Wpisz hasło.")]
        public string Password { get; set; }
    }
}