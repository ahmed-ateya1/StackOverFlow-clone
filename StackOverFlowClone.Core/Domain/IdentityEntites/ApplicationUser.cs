using Microsoft.AspNetCore.Identity;
using StackOverFlowClone.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.Domain.IdentityEntites
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
