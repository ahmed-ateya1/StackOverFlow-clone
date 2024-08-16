using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StackOverFlowClone.Core.Domain.Entites;
using StackOverFlowClone.Core.Domain.IdentityEntites;
using StackOverFlowClone.Core.DTO;
using StackOverFlowClone.Core.Services;
using StackOverFlowClone.Core.ServicesContracts;
using StackOverFlowClone.UI.Models.View_Model;

namespace StackOverFlowClone.UI.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IQuestionServices _questionServices;
        private readonly UserManager<ApplicationUser> _userManager; 
        private readonly IAnswerServices _answerServices;

        public QuestionController(ICategoryServices categoryServices,
            IQuestionServices questionServices,
            UserManager<ApplicationUser> userManager,
            IAnswerServices answerServices)
        {
            _categoryServices = categoryServices;
            _questionServices = questionServices;
            _userManager = userManager;
            _answerServices = answerServices;
        }
        [Route("/")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString = null)
        {
            var questions = await _questionServices.GetAllFilteredQuestions(searchString);
            ViewBag.searchString = searchString;
            var user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                ViewBag.userID = user.Id;
            }
            return View(questions);
        }

        [HttpGet]
        public async Task<IActionResult> AddQuestion()
        {
            var categories = await _categoryServices.GetAllCategories();
            ViewBag.categoryList = categories.Select(x =>
                new SelectListItem() { Text = x.CategoryName , Value = x.CategoryID.ToString() });
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuestion(QuestionAddRequest questionAdd)
        {
            if (!ModelState.IsValid) 
            {
                var categories = await _categoryServices.GetAllCategories();
                ViewBag.categoryList = categories.Select(x =>
                    new SelectListItem() { Text = x.CategoryName, Value = x.CategoryID.ToString() }
                    );
                return View(questionAdd);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            questionAdd.UserID = user.Id;
            await _questionServices.AddQuestionAsync(questionAdd);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> EditQuestion(Guid? questionID)
        {
            var questionResponse = await _questionServices.GetQuestionByIDAsync(questionID);
            if (questionResponse == null)
                return NotFound();

            var categories = await _categoryServices.GetAllCategories();
            ViewBag.categoryList = categories.Select(x =>
                new SelectListItem() { Text = x.CategoryName, Value = x.CategoryID.ToString() }
                );

            var questionRequest = new QuestionUpdateRequest()
            {
                AnswersCount = questionResponse.AnswersCount,
                CategoryID = questionResponse.CategoryID,
                QuestionDateAndTime = questionResponse.QuestionDateAndTime,
                QuestionName = questionResponse.QuestionName,
                UserID = questionResponse.UserID,
                ViewCount = questionResponse.ViewCount,
                VotesCount = questionResponse.VotesCount,
                QuestionID = questionResponse.QuestionID,
            };

            return View(questionRequest);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuestion(QuestionUpdateRequest questionUpdate)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryServices.GetAllCategories();
                ViewBag.categoryList = categories.Select(x =>
                    new SelectListItem() { Text = x.CategoryName, Value = x.CategoryID.ToString() }
                    );
                return View(questionUpdate);
            }
            
            await _questionServices.UpdateQuestionAsync(questionUpdate);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteQuestion(Guid? questionID)
        {
            if (questionID == null)
                return NotFound();

            await _questionServices.DeleteQuestionAsync(questionID);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> QuestionDetails(Guid? questionID)
        {
            if (questionID == null)
            {
                return BadRequest("Question ID cannot be null.");
            }

            var question = await _questionServices.GetQuestionByIDAsync(questionID);

            if (question == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user != null && user.Id != question.UserID)
            {
                ViewBag.userID = user.Id;
                await _questionServices.IncrementViewCountAsync(questionID.Value);
            }

            var answers = await _answerServices.GetAllAnswersForQuestionAsync(questionID.Value);

            var viewModel = new QuestionDetailsViewModel
            {
                Question = question,
                Answers = answers.ToList()
            };

            return View(viewModel);
        }
    }
}
