using Qna.DAL.Generic;
using QnA.BAL.Contract;
using QnA.BAL.DTO;
using QnA.BAL.Services;
using QnA.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.BAL.Implementation
{
    public class AuthBL : IAuthBL
    {
        private readonly IHashingService _hashingService;
        private readonly ITokenServiceProvider _tokenServiceProvider;
        private readonly IRepository<AppUser> _appuserRepo;
        public AuthBL(IHashingService hashingService, ITokenServiceProvider tokenServiceProvider, IRepository<AppUser> appuserRepo)
        {
            _hashingService = hashingService;
            _tokenServiceProvider = tokenServiceProvider;
            _appuserRepo = appuserRepo;
        }

        public async Task<AuthResponseDTO> Login(AuthRequestDTO guest)
        {
            var user = await _appuserRepo.GetByIdAsync(guest.UserName);
            if (user == null) return null;
            bool isAuthorized = _hashingService.HashCheck(user.PasswordHash, guest.Password);
            if (!isAuthorized) return null;
            return _tokenServiceProvider.GenerateAccessToken(user);
        }
    }
}
