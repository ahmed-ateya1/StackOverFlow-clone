using StackOverFlowClone.Core.Domain.Entites;
using StackOverFlowClone.Core.Domain.IdentityEntites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.DTO
{
    public class VoteAddRequest
    {
        public Guid UserID { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public Guid AnswerID { get; set; } = Guid.Empty;

        [Required(ErrorMessage = "{0} is required")]
        public int VoteValue { get; set; }

        public Vote ToVote()
        {
            if (AnswerID == Guid.Empty)
                throw new InvalidOperationException("AnswerID cannot be an empty GUID.");

            return new Vote
            {
                UserID = UserID,
                VoteValue = VoteValue,
                AnswerID = AnswerID
            };
        }
    }

}
