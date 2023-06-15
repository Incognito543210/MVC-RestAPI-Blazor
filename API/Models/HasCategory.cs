namespace API.Models
{
    public class HasCategory
    {
        public int HasCategoryID { get; set; }
        public int RecipeID { get; set; }
        public int IngridientID { get; set; }
        public Recipe Recipe { get; set; }
        public Ingridient Ingridient { get; set; }
    }
}
