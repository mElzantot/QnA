using Qna.DAL.Generic;
using QnA.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qna.DAL.Repos_Interfaces
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        Task<bool> UpdateAnswerVotesCounters(int answerId, int upVotes, int downVotes);

    }
}
