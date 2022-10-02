using Moq;
using Qna.DAL.Repos_Interfaces;
using QnA.BAL.Contract;
using QnA.BAL.DTO;
using QnA.BAL.Implementation;
using QnA.BAL.Services;
using QnA.DbModels;

namespace QnA.Testing
{
    public class AuthBLShould
    {

        [Fact]
        public async Task ReturnNullIfUserNotExist()
        {
            //-------Arrange
            AuthRequestDTO requestdto = new AuthRequestDTO
            {
                UserName = "MohamedShaker",
                Password = "MohamedPassWord"
            };


            Mock<IAppUserRepository> appuserRepo = new Mock<IAppUserRepository>();
            Mock<IHashingService> hashingService = new Mock<IHashingService>();
            Mock<ITokenServiceProvider> tokenService = new Mock<ITokenServiceProvider>();
            IAuthBL authBL = new AuthBL(hashingService.Object, tokenService.Object, appuserRepo.Object);

            appuserRepo.Setup(x => x.GetAsync(x => x.Name == requestdto.UserName)).ReturnsAsync((AppUser)null);

            //----------Act
            var actual = authBL.Login(requestdto).Result;

            //----------Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task ReturnNullIfPasswordisNotCorrect()
        {
            //-------Arrange
            AuthRequestDTO requestdto = new AuthRequestDTO
            {
                UserName = "MohamedShaker",
                Password = "MohamedPassWord"
            };

            AppUser appuser = new AppUser
            {
                Id = "userId",
                Name = requestdto.UserName,
                PasswordHash = "dummyPass"
            };


            Mock<IAppUserRepository> appuserRepo = new Mock<IAppUserRepository>();
            Mock<IHashingService> hashingService = new Mock<IHashingService>();
            Mock<ITokenServiceProvider> tokenService = new Mock<ITokenServiceProvider>();
            IAuthBL authBL = new AuthBL(hashingService.Object, tokenService.Object, appuserRepo.Object);

            appuserRepo.Setup(x => x.GetAsync(x => x.Name == requestdto.UserName)).ReturnsAsync(appuser);
            hashingService.Setup(x => x.HashCheck(appuser.PasswordHash, requestdto.Password)).Returns(false);
            //----------Act
            var actual = authBL.Login(requestdto).Result;

            //----------Assert
            Assert.Null(actual);
        }
    }
}