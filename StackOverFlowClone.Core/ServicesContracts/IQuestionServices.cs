using StackOverFlowClone.Core.Domain.Entites;
using StackOverFlowClone.Core.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.ServicesContracts
{
    /// <summary>
    /// Contract for services related to question operations.
    /// </summary>
    public interface IQuestionServices
    {
        /// <summary>
        /// Adds a new question.
        /// </summary>
        /// <param name="request">The request containing question details.</param>
        /// <returns>The added question's response DTO.</returns>
        Task<QuestionResponse> AddQuestionAsync(QuestionAddRequest? request);

        /// <summary>
        /// Updates an existing question.
        /// </summary>
        /// <param name="questionID">The unique identifier of the question to update.</param>
        /// <param name="request">The request containing updated question details.</param>
        /// <returns>The updated question's response DTO.</returns>
        Task<QuestionResponse> UpdateQuestionAsync(QuestionUpdateRequest? questionRequest);

        /// <summary>
        /// Deletes a question by its unique identifier.
        /// </summary>
        /// <param name="questionID">The unique identifier of the question to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        Task<bool> DeleteQuestionAsync(Guid? questionID);

        /// <summary>
        /// Retrieves all questions.
        /// </summary>
        /// <returns>A collection of response DTOs for all questions.</returns>
        Task<IEnumerable<QuestionResponse>> GetAllQuestionsAsync();

        Task<IEnumerable<QuestionResponse>> GetAllFilteredQuestions(string? searchString);
        /// <summary>
        /// Retrieves all questions for specific user by its unique identifier.
        /// </summary>
        /// <param name="userID">The unique identifier of the question to retrieve.</param>
        /// <returns>A collection of response DTOs for all questions.</returns>
        Task<IEnumerable<QuestionResponse>> GetAllQuestionForSpecificUserAsync(Guid? userID);

        /// <summary>
        /// Retrieves a specific question by its unique identifier.
        /// </summary>
        /// <param name="questionID">The unique identifier of the question to retrieve.</param>
        /// <returns>The response DTO of the retrieved question.</returns>
        Task<QuestionResponse> GetQuestionByIDAsync(Guid? questionID);

        Task<QuestionResponse> GetQuestionByAnswerIdAsync(Guid? answerID);

        /// <summary>
        /// Updates the vote count for a specific question.
        /// </summary>
        /// <param name="questionID">The unique identifier of the question.</param>
        /// <param name="voteValue">The value to increment or decrement the vote count by.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateVotesCountAsync(Guid? questionID, int voteValue);

        /// <summary>
        /// Updates the answers count for a specific question.
        /// </summary>
        /// <param name="questionID">The unique identifier of the question.</param>
        /// <param name="answersCount">The new answers count.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAnswersCountAsync(Guid? questionID, int answersCount);

        /// <summary>
        /// Increments the view count for a specific question.
        /// </summary>
        /// <param name="questionID">The unique identifier of the question.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task IncrementViewCountAsync(Guid? questionID);
    }
}
