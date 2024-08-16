using Microsoft.EntityFrameworkCore;
using StackOverFlowClone.Core.Domain.Entites;
using StackOverFlowClone.Core.Domain.RepositoryContracts;
using StackOverFlowClone.Core.ServicesContracts;
using StackOverFlowClone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Infrastructure.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        /// <summary>
        /// Implements the contract for managing category-related data operations.
        /// </summary>
        private readonly AppDbContext _db;
        private readonly IAnswerServices _answerServices;

        public QuestionRepository(AppDbContext db , IAnswerServices answerServices)
        {
            _db = db;
            _answerServices = answerServices;
        }

        public async Task<Question> CreateQuestion(Question question)
        {
            await _db.Qustions.AddAsync(question);
            await _db.SaveChangesAsync();
            return question;
        }

        public async Task<bool> DeleteQuestion(Guid questionID)
        {
            var question = await GetQuestionByID(questionID);
            if (question == null)
                return false;
            _db.Qustions.Remove(question);
            await _db.SaveChangesAsync();
            return true;
        }

        

        public async Task<IEnumerable<Question>> GetAllQuestionForSpecificUser(Guid userID)
        {
            return await _db.Qustions.Include(x=>x.User)
                .Include(x=>x.Category)
                .Include(x=>x.Answers)
                .Where(x => x.UserID == userID).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
           return await _db.Qustions.Include(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.Answers).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetFilteredQuestions(Expression<Func<Question, bool>> predict)
        {
           return await _db.Qustions.Include(x=>x.User)
                .Include(x=>x.Answers)
                .Include(x=>x.Category)
                .Where(predict)
                .ToListAsync();
        }

        public async Task<Question> GetQuestionByAnswerIdAsync(Guid answerID)
        {
            var answer = await _answerServices.GetAnswerByIDAsync(answerID);
            
            var question = await GetQuestionByID(answer.QuestionID);

            return question;
                
        }

        public async Task<Question> GetQuestionByID(Guid questionID)
        {
            return await _db.Qustions.Include(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.Answers).FirstOrDefaultAsync(x => x.QuestionID == questionID);
        }

        public async Task<Question> UpdateQuestion(Question question)
        {
            var Oldquestion = await GetQuestionByID(question.QuestionID);
            if (Oldquestion == null)
                return null;
            Oldquestion.QuestionName = question.QuestionName;
            Oldquestion.QuestionDateAndTime = question.QuestionDateAndTime;
            Oldquestion.CategoryID = question.CategoryID;
            await _db.SaveChangesAsync();
            return Oldquestion;
        }

        public async Task UpdateQuestionAnswersCount(Guid questionID, int value)
        {
            var question = await GetQuestionByID(questionID);
            if (question == null) return;

            question.AnswersCount += value;
            await _db.SaveChangesAsync();
        }

        public async Task UpdateQuestionViewsCount(Guid questionID)
        {
            var question = await GetQuestionByID(questionID);
            if (question == null) return;
            question.ViewCount += 1;
            await _db.SaveChangesAsync();
        }

        public async Task UpdateQuestionVotesCount(Guid questionID, int value)
        {
            var question = await GetQuestionByID(questionID);
            if (question == null) return;
            question.VotesCount += value;
            await _db.SaveChangesAsync();
        }
    }
}
