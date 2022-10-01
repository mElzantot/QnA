using QnA.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.BAL.Contract
{
    public interface IAuthBL
    {
        Task<AuthResponseDTO> Login(AuthRequestDTO guest);

    }
}
