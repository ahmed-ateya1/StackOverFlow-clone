using StackOverFlowClone.Core.Domain.RepositoryContracts;
using StackOverFlowClone.Core.DTO;
using StackOverFlowClone.Core.Helper;
using StackOverFlowClone.Core.ServicesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.Services
{
    public class AnswerServices : IAnswerServices
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswerServices(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public async Task<AnswerResponse> AddAnswerAsync(AnswerAddRequest? request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            ValidationModel.ValidateModel(request);

            request.AnswerDateAndTime = DateTime.Now;
            var answer = request.ToAnswer();

            answer.AnswerID = Guid.NewGuid();

            await _answerRepository.CreateAnswer(answer);

            return answer.ToAnswerResponse();
        }

        public async Task<bool> DeleteAnswerAsync(Guid? answerID)
        {
            if (answerID == null)
                throw new ArgumentNullException(nameof(answerID));

            var answer = await _answerRepository.GetAnswerByID(answerID.Value);

            if (answer == null)
                return false;

            await _answerRepository.DeleteAnswer(answerID.Value);
            return true;
        }

        public async Task<IEnumerable<AnswerResponse>> GetAllAnswersForQuestionAsync(Guid? questionID)
        {
            if (questionID == null) throw new ArgumentNullException(nameof(questionID));

            var answers = await _answerRepository.GetAllAnswerForQuestion(questionID.Value);
            return answers.Select(x => x.ToAnswerResponse()).ToList();
        }

        public async Task<AnswerResponse> GetAnswerByIDAsync(Guid? answerID)
        {
            if (answerID == null)
                throw new ArgumentNullException(nameof(answerID));

            var answer = await _answerRepository.GetAnswerByID(answerID.Value);

            if (answer == null)
                throw new KeyNotFoundException("Answer not found.");

            return answer.ToAnswerResponse();
        }

        public async Task<AnswerResponse> UpdateAnswerAsync(Guid? answerID, AnswerAddRequest? request)
        {
            if (answerID == null)
                throw new ArgumentNullException(nameof(answerID));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            ValidationModel.ValidateModel(request);

            var answer = await _answerRepository.GetAnswerByID(answerID.Value);

            if (answer == null)
                throw new KeyNotFoundException("Answer not found.");

            answer.AnswerText = request.AnswerText;
            answer.QuestionID = request.QuestionID;
            answer.AnswerDateAndTime = DateTime.Now;
            answer.UserID = request.UserID;

            await _answerRepository.UpdateAnswer(answer);

            return answer.ToAnswerResponse();
        }

        public async Task UpdateVotesCountAsync(Guid? answerID, int voteValue)
        {
            if (answerID == null)
                throw new ArgumentNullException(nameof(answerID));

            await _answerRepository.UpdateVotesCount(answerID.Value, voteValue);
        }
    }
}
