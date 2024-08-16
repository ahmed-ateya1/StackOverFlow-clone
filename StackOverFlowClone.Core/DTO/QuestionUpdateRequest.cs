using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.DTO
{
    public class QuestionUpdateRequest
    {
        public Guid QuestionID { get; set; }
        [Required(ErrorMessage = "Question Name is required")]
        [StringLength(200, ErrorMessage = "Question Name can't be longer than 200 characters")]
        public string QuestionName { get; set; }

        [Required(ErrorMessage = "Question Date and Time is required")]
        public DateTime QuestionDateAndTime { get; set; } = DateTime.Now;

        [Range(0, long.MaxValue, ErrorMessage = "Votes Count must be a positive number")]
        public long VotesCount { get; set; } = 0;

        [Range(0, long.MaxValue, ErrorMessage = "Answers Count must be a positive number")]
        public long AnswersCount { get; set; } = 0;

        [Range(0, long.MaxValue, ErrorMessage = "View Count must be a positive number")]
        public long ViewCount { get; set; } = 0;

        public Guid UserID { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public Guid CategoryID { get; set; }
    }
}
