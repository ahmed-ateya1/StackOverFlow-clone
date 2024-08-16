using Microsoft.EntityFrameworkCore;
using StackOverFlowClone.Core.Domain.Entites;
using StackOverFlowClone.Core.Domain.IdentityEntites;
using StackOverFlowClone.Core.Domain.RepositoryContracts;
using StackOverFlowClone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowClone.Infrastructure.Repositories
{
    /// <summary>
    /// Implements the contract for managing vote-related data operations.
    /// </summary>
    public class VoteRepository : IVoteRepository
    {
        private readonly AppDbContext _db;

        public VoteRepository(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates a new vote.
        /// </summary>
        /// <param name="vote">The vote entity to create.</param>
        /// <returns>The created vote entity.</returns>
        public async Task<Vote> CreateVote(Vote vote)
        {
            await _db.Votes.AddAsync(vote);
            await _db.SaveChangesAsync();
            return vote;
        }

        public async Task<bool> DeleteVote(Guid voteID)
        {
            var vote = await GetVoteByVoteID(voteID);
            if(vote == null)
                return false;
            _db.Votes.Remove(vote);
            await _db.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Retrieves all users who voted on a specific answer.
        /// </summary>
        /// <param name="answerID">The unique identifier of the answer.</param>
        /// <returns>A list of users who voted on the answer.</returns>
        public async Task<IEnumerable<ApplicationUser>> GetAllUserVotedInAnswer(Guid answerID)
        {
            var users = await _db.Votes
                .Where(v => v.AnswerID == answerID)
                .Select(v => v.User)
                .ToListAsync();

            return users ?? new List<ApplicationUser>();
        }

        public async Task<int> GetTotalVotesCount(Guid answerID)
        {
            return await _db.Votes
                .Where(x => x.AnswerID == answerID)
                .SumAsync(x => x.VoteValue);
        }

        /// <summary>
        /// Retrieves a vote based on user ID and answer ID.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <param name="answerID">The unique identifier of the answer.</param>
        /// <returns>The vote entity if found, otherwise null.</returns>
        public async Task<Vote> GetVoteById(Guid userID, Guid answerID)
        {
            return await _db.Votes.Include(x => x.User)
                                  .Include(x => x.Answer)
                                  .FirstOrDefaultAsync(x => x.UserID == userID && x.AnswerID == answerID);
        }

        /// <summary>
        /// Retrieves a vote based on the vote ID.
        /// </summary>
        /// <param name="voteID">The unique identifier of the vote.</param>
        /// <returns>The vote entity if found, otherwise null.</returns>
        public async Task<Vote> GetVoteByVoteID(Guid voteID)
        {
            return await _db.Votes.Include(x => x.User)
                .Include(x => x.Answer).FirstOrDefaultAsync(x => x.VoteID == voteID);
        }

        /// <summary>
        /// Updates an existing vote or creates a new one if it does not exist.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <param name="answerID">The unique identifier of the answer.</param>
        /// <param name="value">The vote value.</param>
        /// <returns>The updated or created vote entity.</returns>
        public async Task<Vote> UpdateVote(Guid userID, Guid answerID, int value)
        {
            var vote = await GetVoteById(userID, answerID);

            if (vote != null)
            {
                vote.VoteValue = NormalizeVoteValue(value);
                await _db.SaveChangesAsync();
            }
            else
            {
                vote = new Vote
                {
                    VoteID = Guid.NewGuid(),
                    AnswerID = answerID,
                    UserID = userID,
                    VoteValue = NormalizeVoteValue(value)
                };
                await CreateVote(vote);
            }

            return vote;
        }

        public async Task<int> UserIsVoted(Guid userID, Guid answerID)
        {
            var vote = await _db.Votes.FirstOrDefaultAsync(x=>x.UserID==userID && x.AnswerID == answerID);
            if( vote == null )
                return 0;
            return vote.VoteValue;
        }

        /// <summary>
        /// Normalizes the vote value to ensure it is either -1, 0, or 1.
        /// </summary>
        /// <param name="value">The input vote value.</param>
        /// <returns>The normalized vote value.</returns>
        private int NormalizeVoteValue(int value)
        {
            return value > 0 ? 1 : value < 0 ? -1 : 0;
        }
    }
}
