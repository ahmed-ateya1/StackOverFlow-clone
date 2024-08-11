using StackOverFlowClone.Core.Domain.IdentityEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.Domain.Entites
{
    public class Vote
    {
        public Guid VoteID { get; set; }
        public Guid UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public Guid AnswerID { get; set; }
        public virtual Answer Answer { get; set; }
        public int VoteValue { get; set; }
    }
}
