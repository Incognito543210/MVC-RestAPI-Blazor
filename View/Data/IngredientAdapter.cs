using Model.DTO;
using System.ComponentModel.DataAnnotations;

namespace View.Data
{
    public class IngredientAdapter : IngridientDto
    {
        [Required(ErrorMessage = "Podaj ilość.")]
        public string Quantity { get; set; } = "";
    }
}
