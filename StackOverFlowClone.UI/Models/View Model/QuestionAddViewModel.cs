using Microsoft.AspNetCore.Mvc.Rendering;
using StackOverFlowClone.Core.DTO;

namespace StackOverFlowClone.UI.Models.View_Model
{
    public class QuestionAddViewModel
    {
        public QuestionAddRequest Question { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }

}
