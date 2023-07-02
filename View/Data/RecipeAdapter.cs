using Model.DTO;

namespace View.Data
{
    public class RecipeAdapter : RecipeDto
    {
        //klasa MyRecipe tworzona jest po to, żeby zmodyfiokować PrepareTime, bo format datetime jest zły


        public TimeSpan? PrepTimeAsTimeSpan
        {
            get => PrepareTime.TimeOfDay;
            set => PrepareTime = new DateTime() + value ?? new DateTime();
        }

        //[Required(MinLengthAttribute = 1)]
        public List<IngridientDto> Ingridients { get; set; } = new();

        public List<TagDto> Tags { get; set; } = new();

    }
}
