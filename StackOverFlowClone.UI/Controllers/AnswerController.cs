﻿using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverFlowClone.Core.Domain.Entites;
using StackOverFlowClone.Core.Domain.IdentityEntites;
using StackOverFlowClone.Core.DTO;
using StackOverFlowClone.Core.ServicesContracts;

namespace StackOverFlowClone.UI.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IQuestionServices _questionServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAnswerServices _answerServices;
        private readonly IVoteServices _voteServices;

        public AnswerController(IQuestionServices questionServices,
            UserManager<ApplicationUser> userManager,
            IAnswerServices answerServices,
            IVoteServices voteServices)
        {
            _questionServices = questionServices;
            _userManager = userManager;
            _answerServices = answerServices;
            _voteServices = voteServices;
        }
        [HttpPost]
        public async Task<IActionResult> ReplyToQuestion(AnswerAddRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();
            request.UserID = user.Id;
            await _answerServices.AddAnswerAsync(request);
            await _questionServices.UpdateAnswersCountAsync(request.QuestionID, 1);
            return RedirectToAction("QuestionDetails", "Question", new { questionID = request.QuestionID });
        }
        [HttpPost]
        public async Task<IActionResult> ActionVote(VoteAddRequest voteAdd)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            voteAdd.UserID = user.Id;
            var existingVote = await _voteServices.GetVoteAsync(user.Id, voteAdd.AnswerID);

            if (existingVote != null && voteAdd.VoteValue == 0)
            {
                await _voteServices.DeleteVoteAsync(existingVote.VoteID);
                await _answerServices.UpdateVotesCountAsync(voteAdd.AnswerID, -existingVote.VoteValue);
            }
            else if (existingVote != null && voteAdd.VoteValue == -1 && existingVote.VoteValue == 1)
            {
                await _voteServices.AddOrUpdateVoteAsync(voteAdd);
                await _answerServices.UpdateVotesCountAsync(voteAdd.AnswerID, voteAdd.VoteValue + voteAdd.VoteValue);
            }
            else if (existingVote != null && voteAdd.VoteValue == 1 && existingVote.VoteValue == -1)
            {
                await _voteServices.AddOrUpdateVoteAsync(voteAdd);
                await _answerServices.UpdateVotesCountAsync(voteAdd.AnswerID, voteAdd.VoteValue + voteAdd.VoteValue);
            }
            else
            {
                await _voteServices.AddOrUpdateVoteAsync(voteAdd);
                await _answerServices.UpdateVotesCountAsync(voteAdd.AnswerID, voteAdd.VoteValue);
            }

            var question = await _questionServices.GetQuestionByAnswerIdAsync(voteAdd.AnswerID);
            if (question == null)
                return NotFound();

            var questionID = question.QuestionID;
            return RedirectToAction("QuestionDetails", "Question", new { questionID });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? answerID)
        {
            var answerResponse = await _answerServices.GetAnswerByIDAsync(answerID);
            if(answerResponse == null) return NotFound();

            var answerUpdateRequest = new AnswerUpdateRequest()
            {
                AnswerID = answerResponse.AnswerID,
                QuestionID = answerResponse.QuestionID,
                VotesCount = answerResponse.VotesCount,
                UserID = answerResponse.UserID,
                AnswerDateAndTime = answerResponse.AnswerDateAndTime,
                AnswerText = answerResponse.AnswerText,
            };

            return View(answerUpdateRequest);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AnswerUpdateRequest request)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }
            await _answerServices.UpdateAnswerAsync(request);
            return RedirectToAction("QuestionDetails", "Question", new { questionID = request.QuestionID });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? answerID)
        {
            var answer = await _answerServices.GetAnswerByIDAsync(answerID);
            if(answer == null) return NotFound();
            var questionID = answer.QuestionID;

            await _answerServices.DeleteAnswerAsync(answerID);
            await _questionServices.UpdateAnswersCountAsync(questionID, -1);
            return RedirectToAction("QuestionDetails", "Question", new { questionID = questionID });
        }

    }
}