using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QnA.BAL.Contract;
using QnA.BAL.DTO;
using QnA.DbModels;
using System.Security.Claims;

namespace QnA.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionBL _questionBL;
        private readonly IAnswerBL _answerBL;

        public QuestionsController(IQuestionBL questionBL, IAnswerBL answerBL)
        {
            _questionBL = questionBL;
            _answerBL = answerBL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _questionBL.GetAllQuestionsAsync();
            if (result == null || result.Count() == 0) return NoContent();
            return Ok(result);
        }

        [HttpGet("{questionId}")]
        public async Task<IActionResult> Get(int questionId)
        {
            var result = await _questionBL.GetQuestionWithAnswersAsync(questionId);
            if (result == null) return NoContent();
            return Ok(result);
        }

        [HttpDelete("{questionId}")]
        public async Task<IActionResult> Delete(int questionId)
        {
            var result = await _questionBL.DeleteQuestion(questionId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string question)
        {
            if (string.IsNullOrEmpty(question)) return BadRequest("Question can not be empty");
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var questionProfile = await _questionBL.AddNewQuestion(question, userId);
            if (questionProfile == null) return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding Question");
            return StatusCode(StatusCodes.Status201Created, questionProfile);
        }

        [HttpPost("{questionId}/answers")]
        public async Task<IActionResult> CreateAnswer([FromBody] AddAnswerDTO answerDTO)
        {
            if (string.IsNullOrEmpty(answerDTO.Body)) return BadRequest("Answer body can not be empty");
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var answerProfile = await _answerBL.AddAnswerAsync(answerDTO, userId);
            if (answerProfile == null) return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding Question");
            return StatusCode(StatusCodes.Status201Created, answerProfile);
        }

        [HttpDelete("{questionId}/answers/{answerId}")]
        public async Task<IActionResult> Delete(int questionId, int answerId)
        {
            var result = await _answerBL.DeleteAnswerAsync(questionId, answerId);
            return Ok(result);
        }

        [HttpPut("{questionId}/answers/{answerId}/votes")]
        public async Task<IActionResult> Vote(int questionId, int answerId, [FromQuery] VoteType vote)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _answerBL.UpdateAnswerVote(answerId, vote, userId);

            //--------If this step will affect performance we can use in as background job by using Hangfire for Ex
            if (result) await _questionBL.UpdateQuestionRank(questionId);
            return Ok(result);
        }

    }
}
