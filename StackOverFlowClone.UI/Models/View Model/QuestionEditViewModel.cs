using Microsoft.AspNetCore.Mvc.Rendering;
using StackOverFlowClone.Core.DTO;

namespace StackOverFlowClone.UI.Models.View_Model
{

    public class QuestionEditViewModel
    {
        public QuestionResponse Question { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }

}
