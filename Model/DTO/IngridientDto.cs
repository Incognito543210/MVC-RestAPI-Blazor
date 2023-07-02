using System.ComponentModel.DataAnnotations;

namespace Model.DTO
{
    public class IngridientDto
    {
        public int IngridientID { get; set; }

        [Required(ErrorMessage = "Nazwij składnik.")]
        public string Name { get; set; } = "";
    }
}
