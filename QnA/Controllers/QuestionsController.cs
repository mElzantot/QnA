using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QnA.BAL.Contract;
using System.Security.Claims;

namespace QnA.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionBL _questionBL;

        public QuestionsController(IQuestionBL questionBL)
        {
            _questionBL = questionBL;
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




    }
}
