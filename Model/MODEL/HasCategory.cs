using System.ComponentModel.DataAnnotations;

namespace Model.MODEL
{
    public class HasCategory
    {
        [Key]
        public int HasCategoryID { get; set; }
        public int RecipeID { get; set; }
        public int TagID { get; set; }
        public Recipe Recipe { get; set; }
        public Tag Tag { get; set; }
    }
}
