namespace API.Models
{
    public class Ingridient
    {
        public int IngridientID { get; set; }
        public string name { get; set; }
        public ICollection<HasIngridient> HasIngridients { get; set; }
    }
}
