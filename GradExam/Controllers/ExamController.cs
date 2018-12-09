/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: ExamController.cs 
// Description: Functions as the main controller for the exam page of the program. Displays and handles 
//              controlling for an index page, editing, viewing or selection exam via regular functions or page
//              redirections. 
// 
// Course: CSCI 3250-001 - Software Engineering I  
// Author: Levi Shelton, SHELTONL@etsu.edu 
// Created: December, October 05, 2018 
// Copyright: Scrum Bags, 2018
// 
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradExam.Models;
using GradExam.Services;
using GradExam.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradExam.Controllers
{
	/// <summary>
		/// Controller class for handling exam functions
		/// </summary>
    [Authorize(Roles = "Admin,Director")]
    public class ExamController : Controller
    {
        IGradeExamRepository context;
		/// <summary>
		/// Constructor for connection to IGradeExamRepository
		/// </summary>
		/// <param name="context">Connects to IGradeExamRepository</param>
		/// <returns></returns>
        public ExamController(IGradeExamRepository context)
        {
            this.context = context;
        }
		/// <summary>
		/// IAction method for returing an index view
		/// </summary>
		/// <returns></returns>
        public IActionResult Index()
        {
            var Exam = context.ReadAllExams();
            return View(Exam);
        }
		/// <summary>
		/// IAction method for returning a view of an exam
		/// </summary>
		/// <param name="id">Id for a exam</param>
		/// <returns></returns>
        public IActionResult ViewExam(int id)
        {
            var exam = context.ReadExam(id);
            var vm = new ViewExamViewModel()
            {
                Exam = exam
            };

            var responses = new Dictionary<int, List<QuestionResponse>>();
            foreach (var item in exam.Student.StudentCourses)
            {
                responses.Add(item.Course.Id, context.ReadResponsesByCourse(item.Course.Id, exam.Id));
            }

            vm.Responses = responses;

            return View(vm);
        }
		/// <summary>
		/// IAction method for posting the results of a viewed exam
		/// </summary>
		/// <param name="vm">ViewExamViewModel</param>
		/// <returns></returns>
        [HttpPost]
        public IActionResult ViewExam(ViewExamViewModel vm)
        {
            //TODO: Fix model binding issue
            var responses = vm.Responses;

            foreach (var item in responses)
            {
                var course = context.ReadResponsesByCourse(item.Key, vm.Exam.Id);

                foreach (var qr in course)
                {
                    foreach (var q in item.Value)
                    {
                        var r = context.ReadResponse(qr.Id);
                        r.Response = q.Response;
                        r.Rating = q.Rating;
                        context.SaveResponse(r);
                    }
                }

            }


            return RedirectToAction("ViewExam", new { id = vm.Exam.Id });
        }
		/// <summary>
		/// IAction method for returning a selected exam
		/// </summary>
		/// <returns></returns>
        public IActionResult SelectExam()
        {
            var listAllStudents = context.ReadAllStudents().ToList();
            ViewBag.Students = listAllStudents;

            return View();
        }
		/// <summary>
		/// IAction method for posting a selected exam
		/// </summary>
		/// <param name="studentId">Id for a student</param>
		/// <returns></returns>
        [HttpPost]
        public IActionResult SelectExam(int studentId)
        {
            var exam = context.CreateExam(studentId);

            return RedirectToAction("ViewExam", new { id = exam.Id });
        }
    }
}