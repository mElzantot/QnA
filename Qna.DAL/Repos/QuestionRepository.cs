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
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public QuestionRepository(AppDbContext appdbcontext) : base(appdbcontext)
        {
        }


        public override async Task<Question?> GetByIdAsync(params object[] id)
        {
            return await entities.Include(q => q.Answers).Where(q => q.Id == (int)id[0])
                                 .FirstOrDefaultAsync();
        }

        public override async Task<ICollection<Question>> GetAllAsync(Expression<Func<Question, bool>> predicate)

        {
            return await entities.Include(q => q.Answers).Where(predicate).OrderByDescending(q => q.Id)
                    .ToListAsync();
        }

        public override async Task<bool> RemoveByIdAsync(params object[] id)
        {
            Question? question = await entities.FindAsync(id);
            if (question == null) return false;
            question.IsDeleted = true;
            question.ModificationDate = DateTime.Now;
            entities.Update(question);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
