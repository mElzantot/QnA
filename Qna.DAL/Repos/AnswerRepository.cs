using Microsoft.EntityFrameworkCore;
using Qna.DAL.DBContext;
using Qna.DAL.Generic;
using Qna.DAL.Repos_Interfaces;
using QnA.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Qna.DAL.Repos
{
    public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
        public AnswerRepository(AppDbContext appdbcontext) : base(appdbcontext)
        {
        }

        public override async Task<bool> RemoveAsync(Answer answer)
        {
            if (answer == null) return false;
            answer.IsDeleted = true;
            answer.ModificationDate = DateTime.Now;
            entities.Update(answer);
            return await context.SaveChangesAsync() > 0;
        }


    }
}
