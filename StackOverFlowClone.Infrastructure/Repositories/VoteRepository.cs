using Microsoft.EntityFrameworkCore;
using StackOverFlowClone.Core.Domain.Entites;
using StackOverFlowClone.Core.Domain.RepositoryContracts;
using StackOverFlowClone.Infrastructure.Data;
using System;
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

        public async Task<Vote> CreateVote(Vote vote)
        {
            await _db.Votes.AddAsync(vote);
            await _db.SaveChangesAsync();
            return vote;
        }

        public async Task<Vote> GetVoteById(Guid userID, Guid answerID)
        {
            return await _db.Votes.FirstOrDefaultAsync(x => x.UserID == userID && x.AnswerID == answerID);
        }

        public async Task<Vote> UpdateVote(Guid userID, Guid answerID, int value)
        {
            int updateValue = value > 0 ? 1 : value < 0 ? -1 : 0;
            var vote = await GetVoteById(userID, answerID);

            if (vote != null)
            {
                vote.VoteValue = updateValue;
                await _db.SaveChangesAsync();
                return vote;
            }

            var newVote = new Vote
            {
                VoteID = Guid.NewGuid(),
                AnswerID = answerID,
                UserID = userID,
                VoteValue = updateValue
            };
            return await CreateVote(newVote);
        }
    }
}
