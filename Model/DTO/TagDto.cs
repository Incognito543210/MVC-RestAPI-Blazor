using System.ComponentModel.DataAnnotations;

namespace Model.DTO
{
    public class TagDto
    {
        public int TagID { get; set; }

        [Required(ErrorMessage = "Wybierz tag.")]
        public string Name { get; set; } = "";
    }
}
