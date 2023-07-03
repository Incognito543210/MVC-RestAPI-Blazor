using Model.DTO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace View.Data
{
    public class RecipeAdapter : RecipeDto
    {
        //klasa MyRecipe tworzona jest po to, żeby zmodyfiokować PrepareTime, bo format datetime jest zły

        [Required(ErrorMessage ="Określ czas przygotowania")]
        public TimeSpan? PrepTimeAsTimeSpan
        {
            get => PrepareTime.TimeOfDay;
            set => PrepareTime = new DateTime() + value ?? new DateTime();
        }

        //[Required]
        //[MinLength(1, ErrorMessage = "Przepis musi posiadać co najmniej jeden składnik.")]
        public List<IngridientDto> Ingridients { get; set; } = new();

        //[Required]
        //[MinLength(1, ErrorMessage = "Przepis musi posiadać co najmniej jeden tag.")]
        public List<TagDto> Tags { get; set; } = new();

    }
}
