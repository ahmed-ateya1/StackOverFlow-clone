using StackOverFlowClone.Core.Domain.Entites;
using System;
using System.ComponentModel.DataAnnotations;

namespace StackOverFlowClone.Core.DTO
{
    /// <summary>
    /// Data Transfer Object for adding a new answer.
    /// </summary>
    public class AnswerAddRequest
    {
        /// <summary>
        /// Text content of the answer.
        /// </summary>
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
        public long VotesCount { get; set; } = 0;

        /// <summary>
        /// Unique identifier of the user posting the answer.
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// Unique identifier of the question to which the answer belongs.
        /// </summary>
        [Required(ErrorMessage = "Question ID is required")]
        public Guid QuestionID { get; set; }

        /// <summary>
        /// Converts the current DTO to an <see cref="Answer"/> entity.
        /// </summary>
        /// <returns>An <see cref="Answer"/> entity.</returns>
        public Answer ToAnswer()
        {
            return new Answer()
            {
                AnswerText = AnswerText,
                AnswerDateAndTime = AnswerDateAndTime,
                VotesCount = VotesCount,
                UserID = UserID,
                QuestionID = QuestionID
            };
        }
    }
}
