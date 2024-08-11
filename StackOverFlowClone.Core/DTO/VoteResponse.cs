using StackOverFlowClone.Core.Domain.Entites;
using StackOverFlowClone.Core.Domain.IdentityEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.DTO
{
    public class VoteResponse
    {
        public Guid VoteID { get; set; }
        public Guid UserID { get; set; }
        public Guid AnswerID { get; set; }
        public int VoteValue { get; set; }
    }
    public static class VoteExtension
    {
        public static VoteResponse ToVoteResponse(this Vote vote)
        {
            return new VoteResponse
            {
                VoteID = vote.VoteID,
                UserID = vote.UserID,
                AnswerID = vote.AnswerID,
                VoteValue = vote.VoteValue
            };
        }
    }
}
