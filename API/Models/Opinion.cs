namespace API.Models
{
    public class Opinion
    {
        public int OpinionID { get; set; }
        public DateTime PostData { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public Recipe Recipe { get; set; }
        public User User { get; set; }
    }
}
