﻿using StackOverFlowClone.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowClone.Core.DTO
{
    public class QuestionResponse
    {
        public Guid QuestionID { get; set; }
        public string QuestionName { get; set; }
        public DateTime QuestionDateAndTime { get; set; }
        public long VotesCount { get; set; }
        public long AnswersCount { get; set; }
        public long ViewCount { get; set; }
        public Guid UserID { get; set; }
        public Guid CategoryID { get; set; }
    }
    public static class QuestionExtension
    {
        public static QuestionResponse ToQuestionResponse(this Question question)
        {
            return new QuestionResponse()
            {
                QuestionDateAndTime = question.QuestionDateAndTime,
                QuestionID = question.QuestionID,
                QuestionName = question.QuestionName,
                AnswersCount = question.AnswersCount,
                VotesCount = question.VotesCount,
                ViewCount = question.ViewCount,
                CategoryID = question.CategoryID,
                UserID = question.UserID
            };
        }
    }
}