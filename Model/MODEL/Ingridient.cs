using System.ComponentModel.DataAnnotations;

namespace Model.MODEL
{
    public class Ingridient
    {
        [Key]
        public int IngridientID { get; set; }
        public string Name { get; set; } = "";
        public ICollection<HasIngridient> HasIngridients { get; set; }
    }
}
