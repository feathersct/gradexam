/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: ErrorController.cs 
// Description: Functions as the main controller for the Error page of the program. Handles redirection in the case of
//              error. 
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
using Microsoft.AspNetCore.Mvc;

namespace GradExam.Controllers
{
	/// <summary>
		/// Controller for returning an error view
		/// </summary>
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}