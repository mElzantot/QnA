using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QnA.BAL.Contract;
using QnA.BAL.DTO;

namespace QnA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthBL _authBL;


        public LoginController(IAuthBL authBL)
        {
            _authBL = authBL;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(AuthRequestDTO authRequestDTO)
        {
            if (await _authBL.CheckIfUserNameExist(authRequestDTO.UserName))
                return BadRequest(new { Errors = "User Name is already exist , Please try another one" });
            var userToken = await _authBL.Register(authRequestDTO);
            return Ok(userToken);
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthRequestDTO authRequestDTO)
        {
            var tokens = await _authBL.Login(authRequestDTO);
            return tokens == null ? BadRequest("User Not Found") : Ok(tokens);
        }
    }
}
