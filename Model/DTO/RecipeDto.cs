using Model.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class RecipeDto
    {

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PrepareTime { get; set; }
        public int Portions { get; set; }
        public int Difficulty { get; set; }
        public User User { get; set; }//change object to name of user only in dto
        public int UserID { get; set; }




    }
}
