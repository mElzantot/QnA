using Qna.DAL.Generic;
using QnA.DAL.DTO;
using QnA.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qna.DAL.Repos_Interfaces
{
    public interface IVoteRepository : IRepository<Vote>
    {
        Task<List<VoteStateDTO>> GetVotesCountForAnswer(int answerId);

    }
}
