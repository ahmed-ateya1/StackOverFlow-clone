using StackOverFlowClone.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.DTO
{
    public class CategoryResponse
    {
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        
        public CategoryUpdateRequest ToCategoryUpdateRequest()
        {
            return new CategoryUpdateRequest()
            {
                CategoryID = CategoryID,
                CategoryName = CategoryName
            };
        }
    }
    public static class CategoryResponseExtension
    {
        public static CategoryResponse ToCategoryResponse(this Category category)
        {
            return new CategoryResponse()
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
            };
        }
    }
}
