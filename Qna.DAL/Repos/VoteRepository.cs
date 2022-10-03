using Microsoft.EntityFrameworkCore;
using Qna.DAL.DBContext;
using Qna.DAL.Generic;
using Qna.DAL.Repos_Interfaces;
using QnA.DAL.DTO;
using QnA.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Qna.DAL.Repos
{
    public class VoteRepository : Repository<Vote>, IVoteRepository
    {
        public VoteRepository(AppDbContext appdbcontext) : base(appdbcontext)
        {
        }

        public async Task<List<VoteStateDTO>> GetVotesCountForAnswer(int answerId)
        {
            return await entities.Where(x => x.AnswerId == answerId)
                          .GroupBy(x => x.VoteType)
                          .Select(votes => new VoteStateDTO
                          {
                              Vote = votes.Key,
                              Count = votes.Count()
                          }).ToListAsync();
        }
    }
}
