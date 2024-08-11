using StackOverFlowClone.Core.Domain.Entites;
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
    public class QuestionServices : IQuestionServices
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionServices(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<QuestionResponse> AddQuestionAsync(QuestionAddRequest? request)
        {
            if(request == null) throw new ArgumentNullException(nameof(request));

            ValidationModel.ValidateModel(request);

            request.QuestionDateAndTime = DateTime.Now;
            var question = request.ToQuestion();

            question.QuestionID = Guid.NewGuid();

            await _questionRepository.CreateQuestion(question);

            return question.ToQuestionResponse();
        }

        public async Task<bool> DeleteQuestionAsync(Guid? questionID)
        {
            if(questionID == null)
                throw new ArgumentNullException(nameof(questionID));

            var question = await _questionRepository.GetQuestionByID(questionID.Value);
            if (question == null)
                throw new ArgumentException();

            await _questionRepository.DeleteQuestion(questionID.Value);
            return true;
        }

        public async Task<IEnumerable<QuestionResponse>> GetAllQuestionsAsync()
        {
            var questions = await _questionRepository.GetAllQuestions();

            return questions.Select(x => x.ToQuestionResponse());
        }

        public async Task<QuestionResponse> GetQuestionByIDAsync(Guid? questionID)
        {
            if(questionID == null)
                throw new ArgumentNullException(nameof(questionID));

            var question = await _questionRepository.GetQuestionByID(questionID.Value);

            if (question == null)
                throw new KeyNotFoundException("question not found.");

            return question.ToQuestionResponse();
        }

        public async Task IncrementViewCountAsync(Guid? questionID)
        {
            if(questionID == null)
                throw new ArgumentNullException(nameof(questionID));

            var question = await _questionRepository.GetQuestionByID(questionID.Value);

            if(question == null)
                throw new ArgumentNullException();

            await _questionRepository.UpdateQuestionViewsCount(questionID.Value); 
        }

        public async Task UpdateAnswersCountAsync(Guid? questionID, int answersCount)
        {
            if (questionID == null)
                throw new ArgumentNullException(nameof(questionID));

            var question = await _questionRepository.GetQuestionByID(questionID.Value);

            if (question == null)
                throw new ArgumentNullException();
            await _questionRepository.UpdateQuestionAnswersCount(questionID.Value, answersCount);
        }

        public async Task<QuestionResponse> UpdateQuestionAsync(Guid? questionID, QuestionAddRequest? request)
        {
            if (questionID == null && request == null)
                throw new ArgumentNullException(nameof(questionID));

            ValidationModel.ValidateModel(request);

            var question = await _questionRepository.GetQuestionByID(questionID.Value);

            if(question == null)
                throw new ArgumentNullException();

            question.QuestionName = request.QuestionName;
            question.QuestionDateAndTime = DateTime.Now;
            question.AnswersCount = request.AnswersCount;
            question.ViewCount = request.ViewCount;
            question.VotesCount = request.VotesCount;
            question.UserID = request.UserID;
            question.CategoryID = request.CategoryID;

            await _questionRepository.UpdateQuestion(question);

            return question.ToQuestionResponse();
        }

        public async Task UpdateVotesCountAsync(Guid? questionID, int voteValue)
        {
            if (questionID == null)
                throw new ArgumentNullException(nameof(questionID));

            var question = await _questionRepository.GetQuestionByID(questionID.Value);

            if (question == null)
                throw new ArgumentNullException();

            await _questionRepository.UpdateQuestionVotesCount(questionID.Value, voteValue);
        }
    }
}
