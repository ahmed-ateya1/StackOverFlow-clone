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

        public async Task<IEnumerable<QuestionResponse>> GetAllFilteredQuestions(string? searchString)
        {
            if (String.IsNullOrEmpty(searchString))
                return await GetAllQuestionsAsync();

            searchString = searchString.ToLower();

            var filterdQuestions = (await _questionRepository
                .GetFilteredQuestions(x => x.QuestionName.ToLower().Contains(searchString))).ToList();

            return filterdQuestions.Select(x=>x.ToQuestionResponse());
        }

        public async Task<IEnumerable<QuestionResponse>> GetAllQuestionForSpecificUserAsync(Guid? userID)
        {
            if (userID == null)
                throw new ArgumentNullException();
            
            var questions = await _questionRepository.GetAllQuestionForSpecificUser(userID.Value);
            
            return questions.Select(x=>x.ToQuestionResponse());
        }

        public async Task<IEnumerable<QuestionResponse>> GetAllQuestionsAsync()
        {
            var questions = await _questionRepository.GetAllQuestions();

            return questions.Select(x => x.ToQuestionResponse());
        }

        public async Task<QuestionResponse> GetQuestionByAnswerIdAsync(Guid? answerID)
        {
            if(!answerID.HasValue)
                throw new ArgumentNullException();

            var question = await _questionRepository.GetQuestionByAnswerIdAsync(answerID.Value);
            if (question == null)
                throw new ArgumentNullException();

            return question.ToQuestionResponse();
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

        public async Task UpdateAnswersCountAsync(Guid? questionID, int answersCount = 1)
        {
            if (questionID == null)
                throw new ArgumentNullException(nameof(questionID));

            var question = await _questionRepository.GetQuestionByID(questionID.Value);

            if (question == null)
                throw new ArgumentNullException();
            await _questionRepository.UpdateQuestionAnswersCount(questionID.Value, answersCount);
        }

        public async Task<QuestionResponse> UpdateQuestionAsync(QuestionUpdateRequest? questionRequest)
        {
            if (questionRequest == null)
                throw new ArgumentNullException(nameof(questionRequest));

            ValidationModel.ValidateModel(questionRequest);
            var question = await _questionRepository.GetQuestionByID(questionRequest.QuestionID);

            if (question == null)
                throw new ArgumentNullException();

            questionRequest.UserID = question.UserID;
            question.QuestionName = questionRequest.QuestionName;
            question.QuestionDateAndTime = DateTime.Now;
            question.CategoryID = questionRequest.CategoryID;

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
