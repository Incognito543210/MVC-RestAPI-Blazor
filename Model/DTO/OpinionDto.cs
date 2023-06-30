namespace Model.DTO
{
    public class OpinionDto
    {
        public int OpinionID { get; set; }
        public string Content { get; set; } = "";
        public int Rate { get; set; }
        public int RecipeID { get; set; }
        public int UserID { get; set; }
    }
}
