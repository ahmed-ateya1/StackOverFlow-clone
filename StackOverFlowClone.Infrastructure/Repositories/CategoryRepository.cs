using Microsoft.EntityFrameworkCore;
using StackOverFlowClone.Core.Domain.Entites;
using StackOverFlowClone.Core.Domain.RepositoryContracts;
using StackOverFlowClone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowClone.Infrastructure.Repositories
{
    /// <summary>
    /// Implements the contract for managing category-related data operations.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;

        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategory(Guid categoryID)
        {
            var category = await GetCategoryById(categoryID);
            if (category == null)
                return false;
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(Guid categoryID)
        {
            return await _db.Categories.FirstOrDefaultAsync(x => x.CategoryID == categoryID);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var oldCategory = await GetCategoryById(category.CategoryID);
            if (oldCategory == null)
                return null;
            oldCategory.CategoryName = category.CategoryName;
            await _db.SaveChangesAsync();
            return oldCategory;
        }
    }
}
