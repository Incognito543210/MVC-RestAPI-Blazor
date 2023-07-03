using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class SessionDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
