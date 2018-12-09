/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: CourseController.cs 
// Description: Functions as the main controller for the course page of the program. Displays and handles 
//              controlling for an index page, details for students/learning outcomes/questions, adding/deleting 
//              students/learning outcomes/questions, adding /deleting concentration via regular functions or page
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
using Microsoft.AspNetCore.Authorization;
using GradExam.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GradExam.Controllers
{
	/// <summary>
		/// Controller for handling the various operations on a course
		/// </summary>
		/// <returns></returns>
    [Authorize(Roles = "Admin,Director")]
    public class CourseController : Controller
    {
        private IGradeExamRepository context;
		/// <summary>
		/// Parameterized constructor for building a connection to IGradeExamRepository
		/// </summary>
		/// <returns></returns>
        public CourseController(IGradeExamRepository context)
        {
            this.context = context;
        }	
		/// <summary>
		/// IAction method for returning a delete view
		/// </summary>
		/// <param name="id">Id for a course</param>
		/// <returns></returns>
        public IActionResult Delete(int id)
        {
            var course = context.ReadCourse(id);
            if (course == null)
            {
                return RedirectToAction("Index");
            }
            return View(course);
        }
		/// <summary>
		/// IAction method for posting the results of an delete
		/// </summary>
		/// <param name="id">Id for a course</param>
		/// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            context.DeleteCourse(id);
            return RedirectToAction("Index");
        }
		/// <summary>
		/// IAction method for creating the displays for students outcomes and answers along with detailing a course
		/// </summary>
		/// <param name="id">Id for a course</param>
		/// <returns></returns>
         public IActionResult Details(int id)
        {
            var listAllStudents = context.ReadAllStudents().ToList();
            var listAllOutcomes = context.ReadAllOutcomes().ToList();
            var listAllQuesAnswers = context.ReadAllQuesAnswers().ToList();

            var listStudents = new List<Student>();
            var listLearnOutcomes = new List<LearnOutcome>();
            var listQuesAnswers = new List<QuesAnswer>();

            var studentsInCourse = new List<Student>();
            foreach(var item in context.ReadCourse(id).StudentCourses)
            {
                studentsInCourse.Add(item.Student);
            }

            foreach(var student in listAllStudents)
            {
                if(!studentsInCourse.Contains(student))
                {
                    listStudents.Add(student);
                }
            }

            var learnOutcomeForCourse = new List<LearnOutcome>();
            foreach (var item in context.ReadCourse(id).LearnOutcomes)
            {
                learnOutcomeForCourse.Add(item);
            }

            foreach (var lo in listAllOutcomes)
            {
                if (!learnOutcomeForCourse.Contains(lo))
                {
                    listLearnOutcomes.Add(lo);
                }
            }

            var quesAnswerForCourse = new List<QuesAnswer>();
            foreach (var item in context.ReadCourse(id).QuesAnswers)
            {
                quesAnswerForCourse.Add(item);
            }

            foreach (var qa in listAllQuesAnswers)
            {
                if (!quesAnswerForCourse.Contains(qa))
                {
                    listQuesAnswers.Add(qa);
                }
            }

            ViewBag.Students = listStudents;
            ViewBag.LearningOutcomes = listLearnOutcomes;
            ViewBag.QuesAnswers = listQuesAnswers;

            var course = context.ReadCourse(id);
            if (course == null)
            {
                return RedirectToAction("Index");
            }
            return View(course);
        }
		/// <summary>
		/// IAction method for returning the view of an add to course
		/// </summary>
		/// <param name="id">Id for a concentration</param>
		/// <returns></returns>
        [Authorize(Roles = "Admin, Instructor")]
        public IActionResult Add()
        {
            // Get the list of Users
            var listInstructors = new List<User>();
            listInstructors = context.ReadAllUsers().ToList();

            ViewBag.Instructors = listInstructors;

            return View();
        }
		/// <summary>
		/// IAction method for posting the results of an add, handles validation
		/// </summary>
		/// <param name="id">Id for a concentration</param>
		/// <returns></returns>
        [HttpPost]
        public IActionResult Add(EditCourseViewModel vm)
        {

            if (ModelState.IsValid)
            {
                vm.Course.Instructor = context.Read(vm.UserId);
                context.AddCourse(vm.Course);
                return RedirectToAction("Index");
            }
            return View(vm);

        }
		/// <summary>
		/// IAction method for returning an index view
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
            IEnumerable<Course> courses = context.ReadAllCourses();

			return View(courses);
		}
		/// <summary>
		/// IAction method for returning a course edit view
		/// </summary>
		/// <param name="id">Id for a course</param>
		/// <returns></returns>
        public IActionResult Edit(int id)
        {

            // Get the list of Users
            var listInstructors = new List<User>();
            listInstructors = context.ReadAllUsers().ToList();

            ViewBag.Instructors = listInstructors;

            var course = context.ReadCourse(id);
            if (course == null)
            {
                return RedirectToAction("Index");
            }

            var vm = new EditCourseViewModel()
            {
                Course = course,
                UserId = course.Instructor.Id

            };

            return View(vm);
        }
		/// <summary>
		/// IAction method for posting a course edit view, handles validation
		/// </summary>
		/// <param name="id">Id for a concentration</param>
		/// <returns></returns>
        [HttpPost]
        public IActionResult Edit(EditCourseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Course.Instructor = context.Read(vm.UserId);
                context.UpdateCourse(vm.Course.Id, vm.Course);
                return RedirectToAction("Index");
            }
            return View(vm);
        }
		/// <summary>
		/// IAction method for returning a student addition to a course
		/// </summary>
		/// <param name="studentId">Id for a student</param>
		/// <param name="courseId">Id for a course</param>
		/// <returns></returns>
        public IActionResult AddStudent(int studentId, int courseId)
        {
            try
            {
                if (!ModelState.IsValid || studentId == 0)
                {
                    return RedirectToAction("Details", new { id = courseId });
                }

                var student = context.ReadStudent(studentId);
                var course = context.ReadCourse(courseId);
                context.AddStudentToACourse(student, course);
            }
            catch(Exception ex)
            {
                return Content(ex.ToString());
            }

            return RedirectToAction("Details", new { id = courseId });
        }
		/// <summary>
		/// IAction method for removing a student from a course
		/// </summary>
		/// <param name="studentId">Id for a student</param>
		/// <param name="courseId">Id for a course</param>
		/// <returns></returns>
        public IActionResult RemoveStudent(int studentId, int courseId)
        {
            try
            {
                var student = context.ReadStudent(studentId);
                var course = context.ReadCourse(courseId);
                context.RemoveStudentFromACourse(student, course);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }

            return RedirectToAction("Details", new { id = courseId });
        }
		/// <summary>
		/// IAction method for adding a learning outcome to a course
		/// </summary>
		/// <param name="courseId">Id for a concentration</param>
		/// <param name="learnOutcomeId">Id for a learning outcome</param>
		/// <returns></returns>
        public IActionResult AddLearningOutcome(int learningOutcomeId, int courseId)
        {
            try
            {
                if(!ModelState.IsValid || learningOutcomeId == 0)
                {
                    return RedirectToAction("Details", new { id = courseId });
                }

                var lo = context.ReadOutcome(learningOutcomeId);
                var course = context.ReadCourse(courseId);
                context.AddLOToACourse(lo, course);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }

            return RedirectToAction("Details", new { id = courseId });
        }
		/// <summary>
		/// IAction method for removing a learning outcome
		/// </summary>
		/// <param name="id">Id for a concentration</param>
		/// <returns></returns>
        public IActionResult RemoveLearningOutcome(int learningOutcomeId, int courseId)
        {
            try
            {
                var lo = context.ReadOutcome(learningOutcomeId);
                var course = context.ReadCourse(courseId);
                context.RemoveLOFromACourse(lo, course);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }

            return RedirectToAction("Details", new { id = courseId });
        }
		/// <summary>
		/// IAction method for adding a question to a course
		/// </summary>
		/// <param name="courseid">Id for a question answer</param>
		/// <param name="questionAnswerid">Id for a course</param>
		/// <returns></returns>
		public IActionResult AddQuestionAnswer(int questionAnswerId, int courseId)
        {
            try
            {
                if(!ModelState.IsValid || questionAnswerId == 0)
                {
                    return RedirectToAction("Details", new { id = courseId });
                }

                var qa = context.ReadQuesAnswer(questionAnswerId);
                var course = context.ReadCourse(courseId);
                context.AddQAToACourse(qa, course);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }

            return RedirectToAction("Details", new { id = courseId });
        }
		/// <summary>
		/// IAction method for removing a question/answer from a course
		/// </summary>
		/// <param name="id">Id for a concentration</param>
		/// <returns></returns>
		public IActionResult RemoveQuestionAnswer(int questionAnswerId, int courseId)
        {
            try
            {
                var qa = context.ReadQuesAnswer(questionAnswerId);
                var course = context.ReadCourse(courseId);
                context.RemoveQAFromACourse(qa, course);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }

            return RedirectToAction("Details", new { id = courseId });
        }
    }
}