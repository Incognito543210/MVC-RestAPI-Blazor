using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.MODEL
{
    public class Recipe
    {
        [Key]
        public int RecipeID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime PrepareTime { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime PostData { get; set; }
        public int Portions { get; set; }
        public int Difficulty { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }
        public ICollection<Opinion> Opinions { get; set; }
        public ICollection<HasIngridient> HasIngridients { get; set; }
        public ICollection<HasCategory> HasCategories { get; set; }

    }
}
