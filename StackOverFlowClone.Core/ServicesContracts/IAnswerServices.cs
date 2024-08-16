using StackOverFlowClone.Core.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.ServicesContracts
{
    /// <summary>
    /// Contract for services related to answer operations.
    /// </summary>
    public interface IAnswerServices
    {
        /// <summary>
        /// Adds a new answer to a question.
        /// </summary>
        /// <param name="request">The request containing answer details.</param>
        /// <returns>The added answer's response DTO.</returns>
        Task<AnswerResponse> AddAnswerAsync(AnswerAddRequest? request);

        /// <summary>
        /// Updates an existing answer.
        /// </summary>
        /// <param name="answerUpdate">The request containing updated answer details.</param>
        /// <returns>The updated answer's response DTO.</returns>
        Task<AnswerResponse> UpdateAnswerAsync(AnswerUpdateRequest? answerUpdate);

        /// <summary>
        /// Deletes an answer by its unique identifier.
        /// </summary>
        /// <param name="answerID">The unique identifier of the answer to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        Task<bool> DeleteAnswerAsync(Guid? answerID);

        /// <summary>
        /// Retrieves all answers for a specific question.
        /// </summary>
        /// <param name="questionID">The unique identifier of the question.</param>
        /// <returns>A collection of response DTOs for the answers to the question.</returns>
        Task<IEnumerable<AnswerResponse>> GetAllAnswersForQuestionAsync(Guid? questionID);

        /// <summary>
        /// Retrieves a specific answer by its unique identifier.
        /// </summary>
        /// <param name="answerID">The unique identifier of the answer to retrieve.</param>
        /// <returns>The response DTO of the retrieved answer.</returns>
        Task<AnswerResponse> GetAnswerByIDAsync(Guid? answerID);

        /// <summary>
        /// Updates the vote count for a specific answer.
        /// </summary>
        /// <param name="answerID">The unique identifier of the answer.</param>
        /// <param name="voteValue">The value to increment or decrement the vote count by.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<bool> UpdateVotesCountAsync(Guid? answerID, int voteValue);
    }
}
