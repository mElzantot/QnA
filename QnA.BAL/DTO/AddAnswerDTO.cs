using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.BAL.DTO
{
    public class AddAnswerDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Body { get; set; }
        [Required]
        public int QuestionId { get; set; }
    }

}
