using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.BAL.DTO
{
    public class QuestionProfile
    {
        public int Id { get; set; }
        public string? Body { get; set; }
        public int Rank { get; set; }
        public List<AnswerProfile> Answers { get; set; }
    }
}
