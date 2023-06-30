using System.ComponentModel.DataAnnotations;

namespace Model.MODEL
{
    public class HasIngridient
    {
        [Key]
        public int HasIngridientID { get; set; }
        public string Amount { get; set; } = "";
        public int RecipeID { get; set; }
        public int IngridientID { get; set; }
        public Recipe Recipe { get; set; }
        public Ingridient Ingridient { get; set; }

        
    }
}
