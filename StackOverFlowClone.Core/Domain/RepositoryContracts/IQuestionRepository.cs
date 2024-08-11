using StackOverFlowClone.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Defines the contract for managing question-related data operations.
    /// </summary>
    public interface IQuestionRepository
    {
        /// <summary>
        /// Creates a new question in the database.
        /// </summary>
        /// <param name="question">The question entity to create.</param>
        /// <returns>The created question entity.</returns>
        Task<Question> CreateQuestion(Question question);

        /// <summary>
        /// Updates an existing question in the database.
        /// </summary>
        /// <param name="question">The updated question entity.</param>
        /// <returns>The updated question entity, or null if the question was not found.</returns>
        Task<Question> UpdateQuestion(Question question);

        /// <summary>
        /// Deletes a question by its unique identifier.
        /// </summary>
        /// <param name="questionID">The unique identifier of the question to delete.</param>
        /// <returns>True if the question was successfully deleted; otherwise, false.</returns>
        Task<bool> DeleteQuestion(Guid questionID);

        /// <summary>
        /// Retrieves a question by its unique identifier.
        /// </summary>
        /// <param name="questionID">The unique identifier of the question to retrieve.</param>
        /// <returns>The question entity with the specified identifier, or null if not found.</returns>
        Task<Question> GetQuestionByID(Guid questionID);

        /// <summary>
        /// Retrieves all questions from the database.
        /// </summary>
        /// <returns>An enumerable collection of all question entities.</returns>
        Task<IEnumerable<Question>> GetAllQuestions();

        /// <summary>
        /// Updates the number of answers for a specific question.
        /// </summary>
        /// <param name="questionID">The unique identifier of the question.</param>
        /// <param name="value">The number of answers to add or subtract.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateQuestionAnswersCount(Guid questionID, int value);

        /// <summary>
        /// Updates the vote count for a specific question.
        /// </summary>
        /// <param name="questionID">The unique identifier of the question.</param>
        /// <param name="value">The number of votes to add or subtract.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateQuestionVotesCount(Guid questionID, int value);

        /// <summary>
        /// Updates the view count for a specific question.
        /// </summary>
        /// <param name="questionID">The unique identifier of the question.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateQuestionViewsCount(Guid questionID);
    }
}
