using System.ComponentModel.DataAnnotations;

namespace Model.DTO
{
    public class OpinionDto
    {        
        public int OpinionID { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Dodaj treść opinii.")]
        public string Content { get; set; } = "";
        public int Rate { get; set; }
        public int RecipeID { get; set; }
        public int UserID { get; set; }
    }
}
