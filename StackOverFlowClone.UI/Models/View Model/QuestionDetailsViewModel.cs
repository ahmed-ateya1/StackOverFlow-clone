using StackOverFlowClone.Core.DTO;

namespace StackOverFlowClone.UI.Models.View_Model
{
    public class QuestionDetailsViewModel
    {
        public QuestionResponse Question { get; set; }
        public IEnumerable<AnswerResponse> Answers { get; set; }
    }

}
