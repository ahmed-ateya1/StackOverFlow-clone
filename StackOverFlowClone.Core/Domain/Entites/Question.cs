using StackOverFlowClone.Core.Domain.IdentityEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.Domain.Entites
{
    public class Question
    {
        public Guid QuestionID { get; set; }
        public string QuestionName { get; set; }
        public DateTime QuestionDateAndTime { get; set; }
        public long VotesCount { get; set; }
        public long AnswersCount { get; set; }
        public long ViewCount { get; set; }
        public Guid UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public Guid CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
