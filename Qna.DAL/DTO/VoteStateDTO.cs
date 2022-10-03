using QnA.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.DAL.DTO
{
    public class VoteStateDTO
    {
        public VoteType Vote { get; set; }
        public int Count { get; set; }
    }
}
