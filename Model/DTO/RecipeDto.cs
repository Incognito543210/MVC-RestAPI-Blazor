using Model.MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class RecipeDto
    {
        public int RecipeID { get; set; }

        [Required(ErrorMessage = "Nazwij przepis.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany.")]
        public string Content { get; set; }

        public DateTime PrepareTime { get; set; }
      
        public DateTime PostData { get; set; }

        [Required(ErrorMessage = "Określ liczbę porcji.")]
        [Range(1, 100)]
        public int Portions { get; set; }

        [Required(ErrorMessage = "Określ stopień trudności.")]
        public int Difficulty { get; set; }


        public User User { get; set; }


        public int UserID { get; set; }




    }
}
