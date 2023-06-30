using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.MODEL
{
    public class Opinion
    {
        [Key]
        public int OpinionID { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime PostData { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public Recipe Recipe { get; set; }
        public int RecipeID { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }
    }
}
