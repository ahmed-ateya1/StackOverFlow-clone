using StackOverFlowClone.Core.Domain.IdentityEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.Domain.Entites
{
    public class Answer
    {
        public Guid AnswerID { get; set; }
        public string AnswerText { get; set; }
        public DateTime AnswerDateAndTime { get; set; }
        public long VotesCount {  get; set; }
        public Guid UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public Guid QuestionID { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();    
    }
}
