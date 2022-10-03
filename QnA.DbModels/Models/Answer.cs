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

        //------ I decide to save number of Up/Down votes here and update it with every change in votes 
         //      instead of calc it every time I need it 
         //------- as I think in SYs like that number of Read operation will be so huge comparing to write operation 
         //--- so I found It is better to keep this info here as it will be easier to access although we consumed more DB space
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
