using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MODEL
{
    public class Opinion
    {
        public int OpinionID { get; set; }
        public DateTime PostData { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public Recipe Recipe { get; set; }
        public int RecipeID { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }
    }
}
