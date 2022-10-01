using AutoMapper;
using Qna.DAL.Repos_Interfaces;
using QnA.BAL.Contract;
using QnA.BAL.DTO;
using QnA.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.BAL.Implementation
{
    public class QuestionBL : IQuestionBL
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IMapper mapper;

        public QuestionBL(IQuestionRepository questionRepository, IMapper mapper)
        {
            this.questionRepository = questionRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<QuestionProfile>> GetAllQuestionsAsync()
        {
            var result = await questionRepository.GetAllAsync(q => q.IsDeleted == false);
            return mapper.Map<IEnumerable<QuestionProfile>>(result);
        }

        public async Task<QuestionProfile> GetQuestionWithAnswersAsync(int id)
        {
            var result = await questionRepository.GetByIdAsync(id);
            return mapper.Map<QuestionProfile>(result);
        }
        public async Task<bool> DeleteQuestion(int id)
        {
            return await questionRepository.RemoveByIdAsync(id);
        }

        public async Task<QuestionProfile> AddNewQuestion(string questionBody, string userId)
        {
            if (string.IsNullOrEmpty(questionBody)) return null;
            Question question = new Question
            {
                Body = questionBody,
                CreationDate = DateTime.Now,
                UserId = userId,
                IsDeleted = false
            };
            question = await questionRepository.AddAsync(question);
            return mapper.Map<QuestionProfile>(question);
        }
    }
}
