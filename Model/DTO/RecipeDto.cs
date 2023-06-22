using Model.MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class RecipeDto
    {
        public int RecipeID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
      
        public DateTime PrepareTime { get; set; }
      
        public DateTime PostData { get; set; }
        public int Portions { get; set; }
        public int Difficulty { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }




    }
}
