using Microsoft.EntityFrameworkCore;
using StackOverFlowClone.Core.Domain.Entites;
using StackOverFlowClone.Core.Domain.RepositoryContracts;
using StackOverFlowClone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public QuestionRepository(AppDbContext db)
        {
            _db = db;
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

        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
           return await _db.Qustions.ToListAsync();
        }

        public async Task<Question> GetQuestionByID(Guid questionID)
        {
            return await _db.Qustions.FirstOrDefaultAsync(x => x.QuestionID == questionID);
        }

        public async Task<Question> UpdateQuestion(Question question)
        {
            var Oldquestion = await GetQuestionByID(question.QuestionID);
            if (Oldquestion == null)
                return null;
            Oldquestion.QuestionName = question.QuestionName;
            Oldquestion.QuestionDateAndTime = question.QuestionDateAndTime;
            Oldquestion.CategoryID = question.CategoryID;
            Oldquestion.AnswersCount = question.AnswersCount;
            Oldquestion.ViewCount = question.ViewCount;
            Oldquestion.VotesCount = question.VotesCount;
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
