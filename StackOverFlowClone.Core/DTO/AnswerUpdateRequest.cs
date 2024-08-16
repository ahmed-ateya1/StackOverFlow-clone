using StackOverFlowClone.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.DTO
{
    public class AnswerUpdateRequest
    {
        public Guid AnswerID { get; set; }
        [Required(ErrorMessage = "Answer Text is required")]
        [StringLength(1000, ErrorMessage = "Answer Text can't be longer than 1000 characters")]
        public string AnswerText { get; set; }

        /// <summary>
        /// Date and time when the answer is posted.
        /// </summary>
        [Required(ErrorMessage = "Answer Date and Time is required")]
        public DateTime AnswerDateAndTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Initial vote count for the answer.
        /// </summary>
        [Range(0, long.MaxValue, ErrorMessage = "Votes Count must be a positive number")]
        public long VotesCount { get; set; }

        /// <summary>
        /// Unique identifier of the user posting the answer.
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// Unique identifier of the question to which the answer belongs.
        /// </summary>
        [Required(ErrorMessage = "Question ID is required")]
        public Guid QuestionID { get; set; }

        public Answer ToAnswer()
        {
            return new Answer()
            {
                UserID = UserID,
                AnswerText = AnswerText,
                AnswerDateAndTime = AnswerDateAndTime,
                VotesCount = VotesCount,
                QuestionID = QuestionID,
                AnswerID = AnswerID
            };
        }
    }
}
