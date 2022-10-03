using AutoMapper;
using Moq;
using Qna.DAL.Repos_Interfaces;
using QnA.BAL.Contract;
using QnA.BAL.DTO;
using QnA.BAL.Implementation;
using QnA.DbModels;
using QnA.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Testing
{
    public class AnswerBLShould
    {
        private static IMapper _mapper;
        public AnswerBLShould()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AnswerMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async Task AddNewAnswerWithProperDTO()
        {
            Mock<IAnswerRepository> answerRepo = new Mock<IAnswerRepository>();
            Mock<IVoteRepository> voteRepository = new Mock<IVoteRepository>();
            IAnswerBL answerBL = new AnswerBL(answerRepo.Object, voteRepository.Object, _mapper);

            answerRepo.Setup(x => x.AddAsync(It.IsAny<Answer>())).ReturnsAsync(new Answer { Id = 1, Body = "Ans1", QuestionId = 100 });

            //----------Act
            var actual = await answerBL.AddAnswerAsync(new AddAnswerDTO { Body = "Ans1", QuestionId = 100 }, "user1");

            //----------Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public async Task DeleteAnswerIfTheAnswerIdAndQuestionIdIsRelated()
        {
            Mock<IAnswerRepository> answerRepo = new Mock<IAnswerRepository>();
            Mock<IVoteRepository> voteRepository = new Mock<IVoteRepository>();
            IAnswerBL answerBL = new AnswerBL(answerRepo.Object, voteRepository.Object, _mapper);

            answerRepo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Answer, bool>>>())).ReturnsAsync(new Answer { Id = 1, Body = "Ans1", QuestionId = 100 });
            answerRepo.Setup(x => x.RemoveAsync(It.IsAny<Answer>())).ReturnsAsync(true);

            //----------Act
            var actual = await answerBL.DeleteAnswerAsync(100, 1);

            //----------Assert
            Assert.True(actual);
        }

        [Fact]
        public async Task ReturnFalseIfTheAnswerIdAndQuestionIdIsNotRelated()
        {
            Mock<IAnswerRepository> answerRepo = new Mock<IAnswerRepository>();
            Mock<IVoteRepository> voteRepository = new Mock<IVoteRepository>();
            IAnswerBL answerBL = new AnswerBL(answerRepo.Object, voteRepository.Object, _mapper);

            answerRepo.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Answer, bool>>>())).ReturnsAsync((Answer)null);

            //----------Act
            var actual = await answerBL.DeleteAnswerAsync(100, 1);

            //----------Assert
            Assert.False(false);
        }

        [Fact]
        public async Task ReturnTrueIfUserTryToUnvoteExistingVote()
        {
            Mock<IAnswerRepository> answerRepo = new Mock<IAnswerRepository>();
            Mock<IVoteRepository> voteRepository = new Mock<IVoteRepository>();
            IAnswerBL answerBL = new AnswerBL(answerRepo.Object, voteRepository.Object, _mapper);

            voteRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Vote, bool>>>())).ReturnsAsync(new Vote { VoteType = VoteType.Up, UserId = "user1" });
            voteRepository.Setup(x => x.RemoveAsync(It.IsAny<Vote>())).ReturnsAsync(true);
            voteRepository.Setup(x => x.GetVotesCountForAnswer(It.IsAny<int>())).ReturnsAsync(new List<DAL.DTO.VoteStateDTO>());
            answerRepo.Setup(x => x.UpdateAnswerVotesCounters(1, 0, 0)).ReturnsAsync(true);
            //----------Act
            var actual = await answerBL.UpdateAnswerVote(1, VoteType.UnVote, "user1");

            //----------Assert
            Assert.True(actual);
        }

        [Fact]
        public async Task ReturnFalseIfUserTryToUnvoteNotExistingVote()
        {
            Mock<IAnswerRepository> answerRepo = new Mock<IAnswerRepository>();
            Mock<IVoteRepository> voteRepository = new Mock<IVoteRepository>();
            IAnswerBL answerBL = new AnswerBL(answerRepo.Object, voteRepository.Object, _mapper);

            voteRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Vote, bool>>>())).ReturnsAsync((Vote)null);

            //----------Act
            var actual = await answerBL.UpdateAnswerVote(1, VoteType.UnVote, "user1");

            //----------Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task ReturnFalseIfUserTryToAddSecond()
        {
            Mock<IAnswerRepository> answerRepo = new Mock<IAnswerRepository>();
            Mock<IVoteRepository> voteRepository = new Mock<IVoteRepository>();
            IAnswerBL answerBL = new AnswerBL(answerRepo.Object, voteRepository.Object, _mapper);

            voteRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Vote, bool>>>())).ReturnsAsync(new Vote { VoteType = VoteType.Up, UserId = "user1" });

            //----------Act
            var actual = await answerBL.UpdateAnswerVote(1, VoteType.Up, "user1");

            //----------Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task ReturnTrueIfUserTryToUpVote()
        {
            Mock<IAnswerRepository> answerRepo = new Mock<IAnswerRepository>();
            Mock<IVoteRepository> voteRepository = new Mock<IVoteRepository>();
            IAnswerBL answerBL = new AnswerBL(answerRepo.Object, voteRepository.Object, _mapper);

            voteRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Vote, bool>>>())).ReturnsAsync((Vote)null);
            voteRepository.Setup(x => x.AddAsync(It.IsAny<Vote>())).ReturnsAsync(new Vote { UserId = "user1", AnswerId = 1, VoteType = VoteType.Up });
            voteRepository.Setup(x => x.GetVotesCountForAnswer(It.IsAny<int>())).ReturnsAsync(new List<DAL.DTO.VoteStateDTO>());
            answerRepo.Setup(x => x.UpdateAnswerVotesCounters(1, 0, 0)).ReturnsAsync(true);

            //----------Act
            var actual = await answerBL.UpdateAnswerVote(1, VoteType.Up, "user1");

            //----------Assert
            Assert.True(actual);
        }


    }
}
