using StackOverFlowClone.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.DTO
{
    public class AnswerResponse
    {
        public Guid AnswerID { get; set; }
        public string AnswerText { get; set; }
        public DateTime AnswerDateAndTime { get; set; }
        public long VotesCount { get; set; }
        public Guid UserID { get; set; }
        public Guid QuestionID { get; set; }
    }
    public static class AnswerResponseExtension
    {
        public static AnswerResponse ToAnswerResponse(this  Answer answer)
        {
            return new AnswerResponse()
            {
                AnswerID = answer.AnswerID,
                AnswerText = answer.AnswerText,
                AnswerDateAndTime = answer.AnswerDateAndTime,
                VotesCount = answer.VotesCount,
                UserID = answer.UserID,
                QuestionID = answer.QuestionID
            };
        }
    }
}
