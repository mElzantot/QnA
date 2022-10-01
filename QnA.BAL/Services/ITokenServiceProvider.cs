using QnA.BAL.DTO;
using QnA.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.BAL.Services
{
    public interface ITokenServiceProvider
    {
        AuthResponseDTO GenerateAccessToken(AppUser appuser);
    }
}
