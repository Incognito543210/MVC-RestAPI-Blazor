namespace API.Models
{
    public class HasIngridient
    {
        public int HasIngridientID { get; set; }
        public string Amonut { get; set; }
        public int RecipeID { get; set; }
        public int IngridientID { get; set; }
        public Recipe Recipe { get; set; }
        public Ingridient Ingridient { get; set; }

        
    }
}
