using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradExam.Models;
using GradExam.Services;
using GradExam.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace GradExam.Controllers
{	
	/// <summary>
		/// Controller for handling the QuesAnswers functions
		/// </summary>
		/// <param name="id">Id for a concentration</param>
		/// <returns></returns>
    [Authorize(Roles = "Instructor,Director,Admin")]
    public class QuesAnswerController : Controller
    {
        private IGradeExamRepository context;
		/// <summary>
		/// Constructor for connection to an IGradeExamRepository
		/// </summary>
		/// <param name="context">connection to an IGradeExamRepository</param>
		/// <returns></returns>
        public QuesAnswerController(IGradeExamRepository context)
        {
            this.context = context;
        }
		/// <summary>
		/// IAction method for returning a QuesAnswer index view
		/// </summary>
		/// <returns></returns>
        public IActionResult Index()
        {
            var model = context.ReadAllQuesAnswers();
            return View(model);
        }
		/// <summary>
		/// IAction method for returning a delete view for a QuesAnswer
		/// </summary>
		/// <param name="id">Id for a QuesAnswer</param>
		/// <returns></returns>
        public IActionResult Delete(int id)
        {
            QuesAnswer ques = context.ReadQuesAnswer(id);
            if (ques == null)
            {
                return RedirectToAction("Index");
            }
            return View(ques);
        }
		/// <summary>
		/// IAction method for handling deletion confirmations 
		/// </summary>
		/// <param name="id">Id for a QuesAnswer<param>
		/// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            context.deleteQuestion(id);
            return RedirectToAction("Index");
        }
		/// <summary>
		/// IAction method for returning a view of details
		/// </summary>
		/// <param name="id">Id for a QuesAnswer</param>
		/// <returns></returns>
        public IActionResult Details(int id)
        {
            QuesAnswer ques = context.ReadQuesAnswer(id);
            if (ques == null)
            {
                return RedirectToAction("Index");
            }
            return View(ques);
        }
		/// <summary>
		/// IAction method for returning the view of details attained from adding a QuesAnswer
		/// </summary>
		/// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            var vm = new AddQuesAnswerViewModel();

            // Get the list of courses for the dropdown
            vm.Courses = context.ReadAllCourses().ToList();
            vm.LearnOutcomes = context.ReadAllOutcomes().ToList();

            return View(vm);
        }
		/// <summary>
		/// IAction method for adding a QuesAnswer to the current ViewModel
		/// </summary>
		/// <param name="vm">ViewModel to add to</param>
		/// <returns></returns>
        [HttpPost]
        public IActionResult Add(AddQuesAnswerViewModel vm)
        {
            //Reinitialize the options for the select lists
            vm.Courses = context.ReadAllCourses().ToList();
            vm.LearnOutcomes = context.ReadAllOutcomes().ToList();

            //TODO: Fix the value cannot be null problem
            if (!ModelState.IsValid)
            {
                return View("Add", vm);
            }

            QuesAnswer Qa = new QuesAnswer
            {
                Question = vm.Question,
                Answer = vm.Answer,
                Course = context.ReadCourse(vm.CourseId),
                LearnOutcome = context.ReadOutcome(vm.LearnOutcomeId)
            };
            context.createQuesetion(Qa);
            return RedirectToAction("Index");
        }
		/// <summary>
		/// IAction method for returning a view of for updating a QuesAnswer
		/// </summary>
		/// <param name="id">Id for a QuesAnswer</param>
		/// <returns></returns>
        public IActionResult Update(int id)
        {
            var model = context.ReadQuesAnswer(id);

            var vm = new EditQuestionAnswerViewModel()
            {
                QuesAnswer = model,
                CourseId = model.Course.Id
            };

            if (model.LearnOutcome != null)
            {
                vm.LearnOutcomeId = model.LearnOutcome.Id;

            }

            // Get the list of courses for the dropdown
            vm.Courses = context.ReadAllCourses().ToList();
            vm.LearnOutcomes = context.ReadAllOutcomes().ToList();

            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(vm);
        }
		/// <summary>
		/// IAction method for posting the details of a QuesAnswer that has been updated
		/// </summary>
		/// <param name="vm">ViewModel</param>
		/// <returns></returns>
        [HttpPost]
        public IActionResult Update(EditQuestionAnswerViewModel vm)
        {
            vm.Courses = context.ReadAllCourses().ToList();
            vm.LearnOutcomes = context.ReadAllOutcomes().ToList();

            // Validate the model
            if (!ModelState.IsValid)
            {
                // Get the view model and return it
                return View("Update", vm);
            }


            // Add the qa
            try
            {
                // TODO: add a boolean to the update questions and answers
                vm.QuesAnswer.Course = context.ReadCourse(vm.CourseId);
                vm.QuesAnswer.LearnOutcome = context.ReadOutcome(vm.LearnOutcomeId);
                context.UpdateQuestion(vm.QuesAnswer.Id, vm.QuesAnswer);
            }
            catch
            {
                // For some reason the system could not create the Q&A. Redirect back to Update.
                ModelState.AddModelError(string.Empty, "Sorry, the system could not create the Q&A requested. Try again later.");
                return View("Update", vm);
            }

            return RedirectToAction("Index");
        }
      
    }
}