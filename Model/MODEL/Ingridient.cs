using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model.MODEL
{
    public class Ingridient
    {
        [Key]
        public int IngridientID { get; set; }
        public string name { get; set; }
        public ICollection<HasIngridient> HasIngridients { get; set; }
    }
}
