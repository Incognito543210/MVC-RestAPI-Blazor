using Model.MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class OpinionDto
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
