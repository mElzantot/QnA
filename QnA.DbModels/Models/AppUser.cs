using System.ComponentModel.DataAnnotations;

namespace QnA.DbModels
{
    public class AppUser
    {
        public AppUser()
        {
            this.Questions = new HashSet<Question>();
            this.Answers = new HashSet<Answer>();
        }

        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
