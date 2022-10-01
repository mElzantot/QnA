using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QnA.DbModels
{
    [Table("Question")]
    public class Question : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }

        [ForeignKey]
        public string UserId { get; set; }
    }
}
