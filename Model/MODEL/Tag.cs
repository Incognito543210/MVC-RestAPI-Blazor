using System.ComponentModel.DataAnnotations;

namespace Model.MODEL
{
    public class Tag
    {
        [Key]
        public int TagID { get; set; }
        public string Name { get; set; }
        public ICollection<HasCategory> HasCategories { get; set; }
    }
}
