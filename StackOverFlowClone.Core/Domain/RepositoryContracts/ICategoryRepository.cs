using StackOverFlowClone.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Defines the contract for managing category-related data operations.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Creates a new category in the database.
        /// </summary>
        /// <param name="category">The category entity to create.</param>
        /// <returns>The created category entity.</returns>
        Task<Category> CreateCategory(Category category);

        /// <summary>
        /// Updates an existing category in the database.
        /// </summary>
        /// <param name="category">The updated category entity.</param>
        /// <returns>The updated category entity, or null if the category was not found.</returns>
        Task<Category> UpdateCategory(Category category);

        /// <summary>
        /// Deletes a category by its unique identifier.
        /// </summary>
        /// <param name="categoryID">The unique identifier of the category to delete.</param>
        /// <returns>True if the category was successfully deleted; otherwise, false.</returns>
        Task<bool> DeleteCategory(Guid categoryID);

        /// <summary>
        /// Retrieves all categories from the database.
        /// </summary>
        /// <returns>An enumerable collection of all category entities.</returns>
        Task<IEnumerable<Category>> GetAllCategories();

        /// <summary>
        /// Retrieves a category by its unique identifier.
        /// </summary>
        /// <param name="categoryID">The unique identifier of the category to retrieve.</param>
        /// <returns>The category entity with the specified identifier, or null if not found.</returns>
        Task<Category> GetCategoryById(Guid categoryID);
    }
}
