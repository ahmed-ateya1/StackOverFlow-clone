using StackOverFlowClone.Core.Domain.Entites;
using System;

namespace StackOverFlowClone.Core.DTO
{
    /// <summary>
    /// Data Transfer Object for representing an answer in response to a request.
    /// </summary>
    public class AnswerResponse
    {
        /// <summary>
        /// Unique identifier for the answer.
        /// </summary>
        public Guid AnswerID { get; set; }

        /// <summary>
        /// Text content of the answer.
        /// </summary>
        public string AnswerText { get; set; }

        /// <summary>
        /// Date and time when the answer was posted.
        /// </summary>
        public DateTime AnswerDateAndTime { get; set; }

        /// <summary>
        /// Number of votes the answer has received.
        /// </summary>
        public long VotesCount { get; set; }

        /// <summary>
        /// Unique identifier of the user who posted the answer.
        /// </summary>
        public Guid UserID { get; set; }

        public string UserName { get; set; }
        /// <summary>
        /// Unique identifier of the question to which the answer belongs.
        /// </summary>
        public Guid QuestionID { get; set; }
    }

    /// <summary>
    /// Extension methods for converting domain entities to DTOs.
    /// </summary>
    public static class AnswerResponseExtension
    {
        /// <summary>
        /// Converts an <see cref="Answer"/> entity to an <see cref="AnswerResponse"/> DTO.
        /// </summary>
        /// <param name="answer">The answer entity to convert.</param>
        /// <returns>An <see cref="AnswerResponse"/> DTO.</returns>
        public static AnswerResponse ToAnswerResponse(this Answer answer)
        {
            return new AnswerResponse()
            {
                AnswerID = answer.AnswerID,
                AnswerText = answer.AnswerText,
                AnswerDateAndTime = answer.AnswerDateAndTime,
                VotesCount = answer.VotesCount,
                UserID = answer.UserID,
                QuestionID = answer.QuestionID,
                UserName = answer.User.UserName??"UnKnown"
            };
        }
    }
}
