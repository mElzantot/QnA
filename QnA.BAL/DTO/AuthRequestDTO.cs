using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.BAL.DTO
{
    public class AuthRequestDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
