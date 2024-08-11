using StackOverFlowClone.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.ServicesContracts
{
    public interface ICategoryServices
    {
        Task<CategoryResponse> CreateCategory(CategoryAddRequest? categoryAddRequest);
        Task<CategoryResponse> UpdateCategory(CategoryUpdateRequest? categoryUpdateRequest);
        Task<bool> DeleteCategory(Guid? categoryId);
        Task<CategoryResponse> GetCategoryByID(Guid? categoryId);
        Task<IEnumerable<CategoryResponse>> GetAllCategories();
    }
}
