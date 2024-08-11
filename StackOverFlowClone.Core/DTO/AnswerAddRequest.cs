using StackOverFlowClone.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.DTO
{
    public class AnswerAddRequest
    {
        public string AnswerText { get; set; }
        public DateTime AnswerDateAndTime { get; set; }
        public long VotesCount { get; set; }
        public Guid UserID { get; set; }
        public Guid QuestionID { get; set; }

        public Answer ToAnswer()
        {
            return new Answer()
            {
                AnswerDateAndTime = AnswerDateAndTime,
                AnswerText = AnswerText,
                VotesCount = VotesCount,
                UserID = UserID,
                QuestionID = QuestionID
            };
        }
    }
}
