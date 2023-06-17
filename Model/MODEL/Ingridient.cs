using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MODEL
{
    public class Ingridient
    {
        public int IngridientID { get; set; }
        public string name { get; set; }
        public ICollection<HasIngridient> HasIngridients { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
