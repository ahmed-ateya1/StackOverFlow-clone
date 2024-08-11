using Microsoft.EntityFrameworkCore;
using StackOverFlowClone.Core.Domain.Entites;
using StackOverFlowClone.Core.Domain.RepositoryContracts;
using StackOverFlowClone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowClone.Infrastructure.Repositories
{
    /// <summary>
    /// Implements the contract for managing answer-related data operations.
    /// </summary>
    public class AnswerRepository : IAnswerRepository
    {
        private readonly AppDbContext _db;

        public AnswerRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Answer> CreateAnswer(Answer answer)
        {
            await _db.Answers.AddAsync(answer);
            await _db.SaveChangesAsync();
            return answer;
        }

        public async Task<bool> DeleteAnswer(Guid answerID)
        {
            var answer = await GetAnswerByID(answerID);
            if (answer == null)
                return false;
            _db.Answers.Remove(answer);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Answer>> GetAllAnswerForQuestion(Guid questionID)
        {
            return await _db.Answers.Where(x => x.QuestionID == questionID).ToListAsync();
        }

        public async Task<IEnumerable<Answer>> GetAllAnswers()
        {
            return await _db.Answers.ToListAsync();
        }

        public async Task<Answer> GetAnswerByID(Guid answerID)
        {
            return await _db.Answers.FirstOrDefaultAsync(x => x.AnswerID == answerID);
        }

        public async Task<Answer> UpdateAnswer(Answer answer)
        {
            var oldAnswer = await GetAnswerByID(answer.AnswerID);
            if (oldAnswer == null)
                return null;
            oldAnswer.AnswerText = answer.AnswerText;
            oldAnswer.AnswerDateAndTime = answer.AnswerDateAndTime;
            oldAnswer.VotesCount = answer.VotesCount;
            await _db.SaveChangesAsync();
            return oldAnswer;
        }

        public async Task UpdateVotesCount(Guid answerID, int value)
        {
            var answer = await GetAnswerByID(answerID);
            if (answer == null) return;
            answer.VotesCount += value;
            await _db.SaveChangesAsync(); 
        }
    }
}
