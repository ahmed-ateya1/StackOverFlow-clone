using StackOverFlowClone.Core.Domain.IdentityEntites;
using StackOverFlowClone.Core.DTO;
using System;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.ServicesContracts
{
    /// <summary>
    /// Interface that defines the contract for vote-related services.
    /// </summary>
    public interface IVoteServices
    {
        /// <summary>
        /// Adds or updates a vote based on the user and answer identifiers.
        /// </summary>
        /// <param name="request">The data transfer object containing vote details.</param>
        /// <returns>A <see cref="VoteResponse"/> representing the created or updated vote.</returns>
        Task<VoteResponse> AddOrUpdateVoteAsync(VoteAddRequest? request);

        /// <summary>
        /// Retrieves a vote by the user and answer identifiers.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <param name="answerID">The unique identifier of the answer.</param>
        /// <returns>A <see cref="VoteResponse"/> representing the vote.</returns>
        Task<VoteResponse> GetVoteAsync(Guid? userID, Guid? answerID);

        /// <summary>
        /// Deletes a vote based on the vote identifier.
        /// </summary>
        /// <param name="voteID">The unique identifier of the vote.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteVoteAsync(Guid? voteID);
        /// <summary>
        /// Retrieves user is voted or not
        /// </summary>
        /// <param name="userID">The unique identifier of the user who voted.</param>
        /// <param name="answerID">The unique identifier of the answer being voted on.</param>
        /// <returns>the vote if found return true else return false</returns>
        Task<int> UserIsVotedAsync(Guid? userID, Guid? answerID);
        Task<VoteResponse> GetVoteByVoteID(Guid? voteID);
        Task<IEnumerable<ApplicationUser>> GetAllUserVotedInAnswer(Guid? answerID);
        Task<int>GetTotalVotesCountAsync(Guid? answerID);
    }
}
