using StackOverFlowClone.Core.Domain.RepositoryContracts;
using StackOverFlowClone.Core.DTO;
using StackOverFlowClone.Core.Helper;
using StackOverFlowClone.Core.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryServices(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> CreateCategory(CategoryAddRequest? categoryAddRequest)
        {
            if (categoryAddRequest == null)
                throw new ArgumentNullException(nameof(categoryAddRequest));

            ValidationModel.ValidateModel(categoryAddRequest);

            var category = categoryAddRequest.ToCategory();

            category.CategoryID = Guid.NewGuid();   

            await _categoryRepository.CreateCategory(category);

            return category.ToCategoryResponse();
        }

        public async Task<bool> DeleteCategory(Guid? categoryId)
        {
            if (categoryId == null) throw new ArgumentNullException();

            var category = await _categoryRepository.GetCategoryById(categoryId.Value);
            if(category == null)
                return false;
            await _categoryRepository.DeleteCategory(categoryId.Value);
            return true;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategories();

            return categories.Select(x=>x.ToCategoryResponse());
        }

        public async Task<CategoryResponse> GetCategoryByID(Guid? categoryId)
        {
            if (categoryId == null) throw new ArgumentNullException();
            var category = await _categoryRepository.GetCategoryById(categoryId.Value);

            return category.ToCategoryResponse();
        }

        public async Task<CategoryResponse> UpdateCategory(CategoryUpdateRequest? categoryUpdateRequest)
        {
            if(categoryUpdateRequest == null)
                throw new ArgumentNullException();

            ValidationModel.ValidateModel(categoryUpdateRequest);
            var category = await _categoryRepository.GetCategoryById(categoryUpdateRequest.CategoryID);

            category.CategoryName = categoryUpdateRequest.CategoryName;

            await _categoryRepository.UpdateCategory(category);

            return category.ToCategoryResponse();
        }
    }
}
