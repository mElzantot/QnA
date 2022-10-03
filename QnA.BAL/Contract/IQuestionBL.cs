using QnA.BAL.DTO;
using QnA.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.BAL.Contract
{
    public interface IQuestionBL
    {
        Task<QuestionProfile> AddNewQuestion(string questionBody, string userId);
        Task<IEnumerable<QuestionProfile>> GetAllQuestionsAsync();
        Task<QuestionProfile> GetQuestionWithAnswersAsync(int id);
        Task<bool> DeleteQuestion(int id);
        Task<bool> UpdateQuestionRank(int questionId);

    }
}
