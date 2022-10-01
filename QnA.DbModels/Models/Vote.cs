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
        [Key]
        public int Id { get; set; }
        [Required]
        public VoteType VoteType { get; set; }
        public int AnswerId { get; set; }
        public string UserId { get; set; }

        [ForeignKey("AnswerId")]
        public virtual Answer Answer { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
    }
}
