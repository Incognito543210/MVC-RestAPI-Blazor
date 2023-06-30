using System.ComponentModel.DataAnnotations;

namespace Model.DTO
{
    public class RecipeDto
    {
        public int RecipeID { get; set; }

        [Required(ErrorMessage = "Nazwij przepis.")]
        public string Title { get; set; } = "";

        public string Content { get; set; } = "";

        public DateTime PrepareTime { get; set; }
      
        public DateTime PostData { get; set; }

        [Required(ErrorMessage = "Określ liczbę porcji.")]
        [Range(1, 100)]
        public int Portions { get; set; }

        [Required(ErrorMessage = "Określ stopień trudności.")]
        public int Difficulty { get; set; }

        public int UserID { get; set; }

        //[Required(MinLengthAttribute = 1)]
        public List<IngridientDto> Ingridients { get; set; } = new();

        public List<TagDto> Tags { get; set; } = new();



    }
}
