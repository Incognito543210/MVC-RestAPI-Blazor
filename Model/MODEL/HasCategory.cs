using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MODEL
{
    internal class HasCategory
    {
        public int HasCategoryID { get; set; }
        public int RecipeID { get; set; }
        public int IngridientID { get; set; }
        public Recipe Recipe { get; set; }
        public Ingridient Ingridient { get; set; }
    }
}
