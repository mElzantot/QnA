using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.DbModels
{
    [Table("Vote")]
    public class Vote
    {
        [Required]
        public VoteType VoteType { get; set; }

        [Required]
        public int AnswerId { get; set; }
        [Required]
        public string UserId { get; set; }

        [ForeignKey("AnswerId")]
        public virtual Answer Answer { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
    }
}
