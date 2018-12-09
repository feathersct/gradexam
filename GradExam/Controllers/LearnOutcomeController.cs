using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradExam.Services;
using GradExam.ViewModels;
using GradExam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GradExam.Controllers
{	/// <summary>
		/// Controller for a learning outcome functionality
		/// </summary>
    [Authorize(Roles = "Admin,Director")]
    public class LearnOutcomeController : Controller
    {
        private IGradeExamRepository context;
		/// <summary>
		/// Constructor for creating IGradeExamRepository connectivity
		/// </summary>
		/// <param name="context">connection to IGradeExamRepository</param>
		/// <returns></returns>
        public LearnOutcomeController(IGradeExamRepository context)
        {
            this.context = context;
        }
		/// <summary>
		/// IAction method for returning a learningOutcome index view
		/// </summary>
		/// <returns></returns>
        public IActionResult Index()
        {
            var model = context.ReadAllOutcomes();
            return View(model);
        }
		/// <summary>
		/// IAction method for returning a deletion view
		/// </summary>
		/// <param name="id">Id for a learningOutcome</param>
		/// <returns></returns>
        public IActionResult Delete(int id)
        {
            LearnOutcome Lout = context.ReadOutcome(id);
            if (Lout == null)
            {
                return RedirectToAction("Index");
            }
            return View(Lout);
        }
		/// <summary>
		/// IAction method for posting the results of a deletion
		/// </summary>
		/// <param name="id">Id for a learningOutcome</param>
		/// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            context.DeleteOutcome(id);
            return RedirectToAction("Index");
        }
		/// <summary>
		/// IAction method for returning details of a learningOutcome
		/// </summary>
		/// <param name="id">Id for a learningOutcome</param>
		/// <returns></returns>
        public IActionResult Details(int id)
        {
            LearnOutcome learn = context.ReadOutcome(id);
            if (learn == null)
            {
                return RedirectToAction("Index");
            }
            return View(learn);
        }
		/// <summary>
		/// IAction method for returning an update of a learningOutcome
		/// </summary>
		/// <param name="id">Id for a learningOutcome</param>
		/// <returns></returns>
        public IActionResult Update(int id)
        {
            // Get the list of Questions and Answers
            var listQA = context.ReadAllQuesAnswers().ToList();
            ViewBag.QAs = listQA;

            var model = context.ReadOutcome(id);

            var vm = new EditLearnOutcomeViewModel()
            {
                LearningOutcome = model,
                //QuestionId = model.QuestionAnswer.Id
            };

            return View(vm);
        }
		/// <summary>
		/// IAction method for posting the results of updating a learningOutcome
		/// </summary>
		/// <param name="vm">viewmodel for the update</param>
		/// <returns></returns>
        [HttpPost]
        public IActionResult Update(EditLearnOutcomeViewModel vm)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                // Get the view model and return it
                return View("Update", vm);
            }

            //Add the question and answer to the learning outcome
            vm.LearningOutcome.QuestionAnswer.Add(context.ReadQuesAnswer(vm.QuestionId));
            context.UpdateOutcome(vm.LearningOutcome.Id, vm.LearningOutcome);
            return RedirectToAction("Index");
        }
		/// <summary>
		/// IAction method for returning an add view with details
		/// </summary>
		/// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            // Get the list of Questions and Answers
            var listQA = context.ReadAllQuesAnswers().ToList();
            ViewBag.QAs = listQA;

            var vm = new AddLearnOutcomeViewModel();
            return View(vm);
        }
		/// <summary>
		/// IAction method for posting the result of adding a learning outcome to a viewmodel
		/// </summary>
		/// <param name="vm">viewmodel for addition</param>
		/// <returns></returns>
        [HttpPost]
        public IActionResult Add(AddLearnOutcomeViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return View("Add", vm);
            }

            if (!ModelState.IsValid)
            {
                return View("Add", vm);
            }

            LearnOutcome Lo = new LearnOutcome
            {
                LearningOutcome = vm.LearningOutcome,
                QuestionAnswer = new List<QuesAnswer>()
            };

            Lo.QuestionAnswer.Add(context.ReadQuesAnswer(vm.QuestionId));

            context.CreateLearningOutcome(Lo);
            return RedirectToAction("Index");
        }

    }
}