using StackOverFlowClone.Core.DTO;

namespace StackOverFlowClone.UI.Models.View_Model
{
    public class QuestionIndexViewModel
    {
        public IEnumerable<QuestionResponse> Questions { get; set; }
        public string SearchString { get; set; }
        public Guid UserID { get; set; }
    }

}
