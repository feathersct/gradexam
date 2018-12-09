/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: StudentController.cs 
// Description: Functions as the main controller for the student page of the program. Displays and handles 
//              controlling for an index page, details for students, adding/deleting 
//              students via regular functions or page redirections. 
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
using Microsoft.AspNetCore.Mvc;

namespace GradExam.Controllers
{	
	/// <summary>
		/// Controller for handling a students functionalities 
		/// </summary>
    [Authorize(Roles = "Admin,Director")]
    public class StudentController : Controller
    {
        private IGradeExamRepository context;
		/// <summary>
		/// Constructor for building a connection to IGradeExamRepository
		/// </summary>
		/// <param name="context">connection to IGradeExamRepository</param>
		/// <returns></returns>
        public StudentController(IGradeExamRepository context)
        {
            this.context = context;
        }
		/// <summary>
		/// IAction method for displaying the view of a delete function
		/// </summary>
		/// <param name="id">Id for a Student</param>
		/// <returns></returns>
        public IActionResult Delete(int id)
        {
            var student = context.ReadStudent(id);
            if (student == null)
            {
                return RedirectToAction("Index");
            }
            return View(student);
        }
		/// <summary>
		/// IAction method for posting the details of deleting a student
		/// </summary>
		/// <param name="id">Id of student</param>
		/// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            context.DeleteStudent(id);
            return RedirectToAction("Index");
        }
		/// <summary>
		/// IAction method for returning a view of details
		/// </summary>
		/// <param name="id">Id for a student</param>
		/// <returns></returns>
        public IActionResult Details(int id)
        {
            var student = context.ReadStudent(id);
            if (student == null)
            {
                return RedirectToAction("Index");
            }
            return View(student);
        }

        /// <summary>
		/// IAction method for returning the view of an addition
		/// </summary>
		/// <returns></returns>
        public IActionResult Add()
        {
            return View();
        }
		/// <summary>
		/// IAction method for posting the details of adding a student
		/// </summary>
		/// <param name="student">Student to add</param>
		/// <returns></returns>
        [HttpPost]
        public IActionResult Add(Student student)
        {

            if (ModelState.IsValid)
            {
                context.AddStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);

        }
		/// <summary>
		/// IAction method for returning the an index view
		/// </summary>
		/// <returns></returns>
        public IActionResult Index()
        {
            IEnumerable<Student> students = context.ReadAllStudents();

            return View(students);
        }
		/// <summary>
		/// IAction method for returning an edit view
		/// </summary>
		/// <param name="id">Id for a student</param>
		/// <returns></returns>
        public IActionResult Edit(int id)
        {
            var student = context.ReadStudent(id);
            if (student == null)
            {
                return RedirectToAction("Index");
            }
            return View(student);
        }
		/// <summary>
		/// IAction method for posting the details of editing a student
		/// </summary>
		/// <param name="student">student to be edited</param>
		/// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                context.UpdateStudent(student.Id, student);
                return RedirectToAction("Index");
            }
            return View(student);
        }
    }
}