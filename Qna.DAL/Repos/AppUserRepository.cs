using Microsoft.EntityFrameworkCore;
using Qna.DAL.DBContext;
using Qna.DAL.Generic;
using Qna.DAL.Repos_Interfaces;
using QnA.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qna.DAL.Repos
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(AppDbContext appdbcontext) : base(appdbcontext)
        {
        }

        public async Task<bool> IsUserNameExist(string userName)
        {
            return await entities.AnyAsync(x => x.Name == userName);
        }
    }
}
