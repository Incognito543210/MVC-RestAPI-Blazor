using Model.MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class IngridientDto
    {
        public int IngridientID { get; set; }

        [Required(ErrorMessage = "Nazwij składnik.")]
        public string Name { get; set; }
     


    }
}
