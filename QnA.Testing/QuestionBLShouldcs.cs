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
    public class QuestionBLShould
    {

        private static IMapper _mapper;
        public QuestionBLShould()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new QuestionMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async Task ReturnQuestionsIfThereIsanExistingRecordsfromDB()
        {
            Mock<IQuestionRepository> questionRepo = new Mock<IQuestionRepository>();
            IQuestionBL questionBL = new QuestionBL(questionRepo.Object, _mapper);

            questionRepo.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Question, bool>>>())).ReturnsAsync(new Question[] { new Question { Id = 1 } });

            //----------Act
            var actual = await questionBL.GetAllQuestionsAsync();

            //----------Assert
            Assert.IsType<List<QuestionProfile>>(actual);
        }

        [Fact]
        public async Task ReturnQuestionIfItisExistinDB()
        {
            Mock<IQuestionRepository> questionRepo = new Mock<IQuestionRepository>();
            IQuestionBL questionBL = new QuestionBL(questionRepo.Object, _mapper);

            questionRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Question { Id = 1, Body = "TestQuestion", QuestionRank = 50 });

            QuestionProfile expected = new QuestionProfile { Id = 1, Body = "TestQuestion", Rank = 50 };

            //----------Act
            var actual = await questionBL.GetQuestionWithAnswersAsync(1);

            //----------Assert
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Body, actual.Body);
            Assert.Equal(expected.Rank, actual.Rank);
        }

        [Fact]
        public async Task ReturnNullIfTheQuestionIdIsWrong()
        {
            Mock<IQuestionRepository> questionRepo = new Mock<IQuestionRepository>();
            IQuestionBL questionBL = new QuestionBL(questionRepo.Object, _mapper);

            questionRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Question)null);

            //----------Act
            var actual = await questionBL.GetQuestionWithAnswersAsync(1);

            //----------Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task DeleteQuestionIfIsExist()
        {
            Mock<IQuestionRepository> questionRepo = new Mock<IQuestionRepository>();
            IQuestionBL questionBL = new QuestionBL(questionRepo.Object, _mapper);

            questionRepo.Setup(x => x.RemoveByIdAsync(It.IsAny<int>())).ReturnsAsync(true);

            //----------Act
            var actual = await questionBL.DeleteQuestion(1);
            //----------Assert
            Assert.True(actual);
        }

        [Fact]
        public async Task ReturnFalseIfTheQuestionIsNotExist()
        {
            Mock<IQuestionRepository> questionRepo = new Mock<IQuestionRepository>();
            IQuestionBL questionBL = new QuestionBL(questionRepo.Object, _mapper);

            questionRepo.Setup(x => x.RemoveByIdAsync(It.IsAny<int>())).ReturnsAsync(false);

            //----------Act
            var actual = await questionBL.DeleteQuestion(1);
            //----------Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task ReturnNullWhenBodyiSEmpty()
        {
            Mock<IQuestionRepository> questionRepo = new Mock<IQuestionRepository>();
            IQuestionBL questionBL = new QuestionBL(questionRepo.Object, _mapper);
            Question newQuestion = new Question { Body = String.Empty, UserId = "user1" };
            //----------Act
            var actual = await questionBL.AddNewQuestion(newQuestion.Body, newQuestion.UserId);
            //----------Assert
            Assert.Null(actual);
        }
    }
}
