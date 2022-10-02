using QnA.BAL.DTO;
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
        Task<AnswerProfile> AddAnswerAsync(AddAnswerDTO answerDTO, string userId);
        Task<bool> UpdateAnswerVote(int id, VoteType vote, string userId);
        Task<bool> DeleteAnswerAsync(int questionId, int answerId);
    }
}
