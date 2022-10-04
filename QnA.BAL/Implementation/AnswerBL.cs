using AutoMapper;
using Qna.DAL.Generic;
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
    public class AnswerBL : IAnswerBL
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IVoteRepository _voteRepository;
        private readonly IMapper _mapper;

        public AnswerBL(IAnswerRepository answerRepository, IVoteRepository voteRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _voteRepository = voteRepository;
            _mapper = mapper;
        }


        public async Task<AnswerProfile> AddAnswerAsync(AddAnswerDTO answerDTO, string userId)
        {
            Answer answer = _mapper.Map<Answer>(answerDTO);
            answer.UserId = userId;
            answer = await _answerRepository.AddAsync(answer);
            return _mapper.Map<AnswerProfile>(answer);
        }

        public async Task<bool> DeleteAnswerAsync(int questionId, int answerId)
        {
            //------ Will be better to recive answer id only as there is no need to question id here 
            //-------What if user sent incorrect question id ? --> Now we had to check if the answer and question are related 

            //-------Check that answer is related to this question 
            Answer answer = await GetAnswer(questionId, answerId);
            if (answer == null) return false;
            return await _answerRepository.RemoveAsync(answer);
        }

        //--------- Related to Updating Answer Up/Down votes
        //-------- We can Use a trigger to update those records 
        //------- Trigger after (Insert , Update , Delete ) on Vote Table 
        //------- but I perfer to keep all my BL in Code however that is not affecting app performance 

        public async Task<bool> UpdateAnswerVote(int questionId, int answerId, VoteType vote, string userId)
        {
            var answer = await GetAnswer(questionId, answerId);
            if (answer == null) return false;

            bool isUpdated = false;
            Vote previousVote = await GetUserVoteOnSameAnswerIfExist(userId, answerId);
            if (previousVote != null && vote == VoteType.UnVote)
            {
                isUpdated = await _voteRepository.RemoveAsync(previousVote);
                if (previousVote.VoteType == VoteType.Up) answer.UpVotes--;
                else answer.DownVotes--;
                await _answerRepository.UpdateAsync(answer);
            }
            else if (previousVote == null && vote != VoteType.UnVote)
            {
                var newVote = await _voteRepository.AddAsync(new Vote
                {
                    AnswerId = answerId,
                    UserId = userId,
                    VoteType = vote
                });
                isUpdated = newVote != null;

                if (isUpdated && vote == VoteType.Up)
                {
                    answer.UpVotes++;
                    await _answerRepository.UpdateAsync(answer);
                }
                else if (isUpdated && vote == VoteType.Down)
                {
                    answer.DownVotes++;
                    await _answerRepository.UpdateAsync(answer);
                }
            }
            return isUpdated;
        }


        private async Task<Answer> GetAnswer(int questionId, int answerId)
        {
            return await _answerRepository.GetAsync(v => v.Id == answerId && v.QuestionId == questionId);
        }
        private async Task<Vote> GetUserVoteOnSameAnswerIfExist(string userId, int answerId)
        {
            return await _voteRepository.GetAsync(v => v.UserId == userId && v.AnswerId == answerId);
        }


    }
}
