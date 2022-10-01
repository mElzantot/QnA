using Qna.DAL.Generic;
using Qna.DAL.Repos_Interfaces;
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
        private readonly IAppUserRepository _appuserRepo;
        public AuthBL(IHashingService hashingService, ITokenServiceProvider tokenServiceProvider, IAppUserRepository appuserRepo)
        {
            _hashingService = hashingService;
            _tokenServiceProvider = tokenServiceProvider;
            _appuserRepo = appuserRepo;
        }

        public async Task<AuthResponseDTO> Register(AuthRequestDTO newGuest)
        {
            var appUser = await _appuserRepo.AddAsync(new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Name = newGuest.UserName,
                PasswordHash = _hashingService.Hash(newGuest.Password),
            });
            if (appUser == null) return null;
            return _tokenServiceProvider.GenerateAccessToken(appUser);
        }


        public async Task<AuthResponseDTO?> Login(AuthRequestDTO guest)
        {
            var user = await _appuserRepo.Get(u => u.Name == guest.UserName);
            if (user == null) return null;
            bool isAuthorized = _hashingService.HashCheck(user.PasswordHash, guest.Password);
            if (!isAuthorized) return null;
            return _tokenServiceProvider.GenerateAccessToken(user);
        }
        public async Task<bool> CheckIfUserNameExist(string userName)
        {
            return await _appuserRepo.IsUserNameExist(userName);
        }


    }
}
