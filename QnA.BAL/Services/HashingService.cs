using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace QnA.BAL.Services
{
    public class HashingService : IHashingService
    {
        public string Hash(string text)
        {
            return Crypto.HashPassword(text);
        }

        public bool HashCheck(string hashed , string text)
        {
            return Crypto.VerifyHashedPassword(hashed , text);
        }

    }
}
