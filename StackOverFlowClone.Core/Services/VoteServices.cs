using StackOverFlowClone.Core.Domain.IdentityEntites;
using StackOverFlowClone.Core.Domain.RepositoryContracts;
using StackOverFlowClone.Core.DTO;
using StackOverFlowClone.Core.Helper;
using StackOverFlowClone.Core.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.Services
{
    public class VoteServices : IVoteServices
    {
        private readonly IVoteRepository _voteRepository;

        public VoteServices(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public async Task<VoteResponse> AddOrUpdateVoteAsync(VoteAddRequest? request)
        {
            if(request == null) throw new ArgumentNullException(nameof(request));

            ValidationModel.ValidateModel(request);
            
            var vote = request.ToVote();
            
            vote.VoteID = Guid.NewGuid();
            
            await _voteRepository.UpdateVote(vote.UserID,vote.AnswerID,vote.VoteValue);

            return vote.ToVoteResponse();
        }

        public async Task<bool> DeleteVoteAsync(Guid? voteID)
        {
            if(voteID == null)
                throw new ArgumentNullException(nameof(voteID));

            var vote = await _voteRepository.GetVoteByVoteID(voteID.Value);
            if (vote == null)
                throw new ArgumentException();



            return await _voteRepository.DeleteVote(vote.VoteID);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUserVotedInAnswer(Guid? answerID)
        {
            if(answerID == null)
                throw new ArgumentNullException(nameof(answerID));

            return await _voteRepository.GetAllUserVotedInAnswer(answerID.Value);
        }

        public async Task<int> GetTotalVotesCountAsync(Guid? answerID)
        {
            if(answerID == null)
                throw new ArgumentNullException(nameof(answerID));
            return await _voteRepository.GetTotalVotesCount(answerID.Value);
        }

        public async Task<VoteResponse> GetVoteAsync(Guid? userID, Guid? answerID)
        {
            if (userID == null || answerID == null)
                throw new ArgumentNullException(userID == null ? nameof(userID) : nameof(answerID));

            var vote = await _voteRepository.GetVoteById(userID.Value, answerID.Value);
            return vote?.ToVoteResponse();
        }

        public async Task<VoteResponse> GetVoteByVoteID(Guid? voteID)
        {
            if( voteID == null)
                throw new ArgumentNullException(nameof(voteID));

            var vote = await _voteRepository.GetVoteByVoteID(voteID.Value);

            return vote.ToVoteResponse();
        }

        public async Task<int> UserIsVotedAsync(Guid? userID, Guid? answerID)
        {
           if(userID == null || answerID == null)
                throw new ArgumentNullException(nameof(userID));

           return await _voteRepository.UserIsVoted(userID.Value,answerID.Value);
        }
    }
}
