using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.Domain.Entites
{
    public class Category
    {
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
