using QnA.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.BAL.Contract
{
    public interface IAnswerBL
    {
        Task<Answer> AddAnswerAsync();
        Task<bool> UpdateAnswerVote();
        Task<bool> DeleteAnswerAsync();
    }
}
