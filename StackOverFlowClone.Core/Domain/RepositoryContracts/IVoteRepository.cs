using StackOverFlowClone.Core.Domain.Entites;
using StackOverFlowClone.Core.Domain.IdentityEntites;
using System;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Defines the contract for managing vote-related data operations.
    /// </summary>
    public interface IVoteRepository
    {
        /// <summary>
        /// Creates a new vote in the database.
        /// </summary>
        /// <param name="vote">The vote entity to create.</param>
        /// <returns>The created vote entity.</returns>
        Task<Vote> CreateVote(Vote vote);

        /// <summary>
        /// Updates an existing vote in the database.
        /// </summary>
        /// <param name="userID">The unique identifier of the user voting.</param>
        /// <param name="answerID">The unique identifier of the answer being voted on.</param>
        /// <param name="value">The new vote value.</param>
        /// <returns>The updated vote entity, or a new vote entity if the vote did not exist.</returns>
        Task<Vote> UpdateVote(Guid userID, Guid answerID, int value);

        /// <summary>
        /// Retrieves a vote by user and answer identifiers.
        /// </summary>
        /// <param name="userID">The unique identifier of the user who voted.</param>
        /// <param name="answerID">The unique identifier of the answer being voted on.</param>
        /// <returns>The vote entity with the specified user and answer identifiers, or null if not found.</returns>
        Task<Vote> GetVoteById(Guid userID, Guid answerID);
        Task<Vote> GetVoteByVoteID(Guid voteID);
        /// <summary>
        /// Retrieves user is voted or not
        /// </summary>
        /// <param name="userID">The unique identifier of the user who voted.</param>
        /// <param name="answerID">The unique identifier of the answer being voted on.</param>
        /// <returns>the vote if found return true else return false</returns>
        Task<int> UserIsVoted(Guid userID, Guid answerID);

        Task<IEnumerable<ApplicationUser>> GetAllUserVotedInAnswer(Guid answerID);

        Task<bool> DeleteVote(Guid voteID);
        Task<int> GetTotalVotesCount(Guid answerID);
    }
}
