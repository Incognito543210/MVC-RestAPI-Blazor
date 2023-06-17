using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.MODEL
{
    public class Tag
    {
        public int TagID { get; set; }
        public string Name { get; set; }
        public ICollection<HasCategory> HasCategories { get; set; }
    }
}
