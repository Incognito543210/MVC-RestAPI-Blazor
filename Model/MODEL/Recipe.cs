using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MODEL
{
    internal class Recipe
    {
        public int RecipeID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PrepareTime { get; set; }
        public DateTime PostData { get; set; }
        public int Portions { get; set; }
        public int Difficulty { get; set; }
        public User User { get; set; }
        public ICollection<Opinion> Opinions { get; set; }
        public ICollection<HasIngridient> HasIngridients { get; set; }
        public ICollection<HasCategory> HasCategories { get; set; }
    }
}
