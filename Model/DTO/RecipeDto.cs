using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.DTO
{
    public class RecipeDto
    {
        public int RecipeID { get; set; }

        [Required(ErrorMessage = "Nazwij przepis.")]
        [MinLength(1)]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Dodaj opis.")]
        [MinLength(1)]
        public string Content { get; set; } = "";

        [Required(ErrorMessage = "Określ czas przygotowania.")]
        public DateTime PrepareTime { get; set; }
      
        public DateTime PostData { get; set; }

        [Required(ErrorMessage = "Określ liczbę porcji.")]
        [Range(1, 100)]
        [DefaultValue(1)]
        public int Portions { get; set; }

        [Required(ErrorMessage = "Określ stopień trudności.")]
        public int Difficulty { get; set; }

        public int UserID { get; set; }
    }
}
