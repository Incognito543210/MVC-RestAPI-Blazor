using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class TagDto
    {
        public int TagID { get; set; }

        [Required(ErrorMessage = "Nazwij tag.")]
        public string Name { get; set; }
    }
}
