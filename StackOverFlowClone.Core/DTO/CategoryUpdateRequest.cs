using StackOverFlowClone.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.DTO
{
    public class CategoryUpdateRequest
    {
        public Guid CategoryID { get; set; }
        [Required(ErrorMessage = "Category Name can't be blank")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        public Category ToCategory()
        {
            return new Category()
            {
                CategoryName = CategoryName
            };
        }
    }
}
