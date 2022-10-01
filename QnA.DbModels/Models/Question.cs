using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QnA.DbModels
{
    [Table("Question")]
    public class Question : BaseEntity
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }
        public int QuestionRank { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
