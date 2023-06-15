namespace API.Models
{
    public class Tag
    {
        public int TagID { get; set; }
        public string Name { get; set; }
        public ICollection<HasCategory> HasCategories { get; set; }
    }
}
