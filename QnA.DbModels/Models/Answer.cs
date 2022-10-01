using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QnA.DbModels
{
    [Table("Answer")]
    public class Answer : BaseEntity
    {
        public Answer()
        {
            this.Votes = new HashSet<Vote>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }

        [Required]
        public string UserId { get; set; }
        public int QuestionId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

    }
}
