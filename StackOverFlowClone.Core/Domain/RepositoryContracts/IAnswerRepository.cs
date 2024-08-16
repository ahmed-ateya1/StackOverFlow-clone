using StackOverFlowClone.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Defines the contract for managing answer-related data operations.
    /// </summary>
    public interface IAnswerRepository
    {
        /// <summary>
        /// Creates a new answer in the database.
        /// </summary>
        /// <param name="answer">The answer entity to create.</param>
        /// <returns>The created answer entity.</returns>
        Task<Answer> CreateAnswer(Answer answer);

        /// <summary>
        /// Updates an existing answer in the database.
        /// </summary>
        /// <param name="answer">The updated answer entity.</param>
        /// <returns>The updated answer entity, or null if the answer was not found.</returns>
        Task<Answer> UpdateAnswer(Answer answer);

        /// <summary>
        /// Deletes an answer by its unique identifier.
        /// </summary>
        /// <param name="answerID">The unique identifier of the answer to delete.</param>
        /// <returns>True if the answer was successfully deleted; otherwise, false.</returns>
        Task<bool> DeleteAnswer(Guid answerID);

        /// <summary>
        /// Retrieves all answers from the database.
        /// </summary>
        /// <returns>An enumerable collection of all answer entities.</returns>
        Task<IEnumerable<Answer>> GetAllAnswers();

        /// <summary>
        /// Retrieves all answers associated with a specific question.
        /// </summary>
        /// <param name="questionID">The unique identifier of the question.</param>
        /// <returns>An enumerable collection of answer entities for the specified question.</returns>
        Task<IEnumerable<Answer>> GetAllAnswerForQuestion(Guid questionID);

        /// <summary>
        /// Retrieves an answer by its unique identifier.
        /// </summary>
        /// <param name="answerID">The unique identifier of the answer to retrieve.</param>
        /// <returns>The answer entity with the specified identifier, or null if not found.</returns>
        Task<Answer> GetAnswerByID(Guid answerID);

        /// <summary>
        /// Updates the vote count for a specific answer.
        /// </summary>
        /// <param name="answerID">The unique identifier of the answer.</param>
        /// <param name="value">The number of votes to add or subtract.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<bool> UpdateVotesCount(Guid answerID, int value);
    }
}
