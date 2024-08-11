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
        [Required(ErrorMessage = "{0} is required")]
        public Guid UserID { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public Guid AnswerID { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int VoteValue { get; set; }

        public Vote ToVote()
        {
            return new Vote
            {
                UserID = UserID,
                VoteValue = VoteValue,
                AnswerID = AnswerID
            };
        }
    }
}
