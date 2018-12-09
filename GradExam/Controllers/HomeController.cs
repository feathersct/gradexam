using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GradExam.Models;
using GradExam.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using GradExam.ViewModels;

namespace GradExam.Controllers
{
	/// <summary>
		/// Controller for handling the home page functions
		/// </summary>
    public class HomeController : Controller
    {
        private IGradeExamRepository context;
		/// <summary>
		/// Constructor for connecting to an IGradeExamRepository
		/// </summary>
		/// <returns></returns>
        public HomeController(IGradeExamRepository context)
        {
            this.context = context;
        }
		/// <summary>
		/// IAction method for a view of the index
		/// </summary>
		/// <returns></returns>
        public IActionResult Index()
        {
            var vm = new HomeIndexViewModel
            {
                Users = context.ReadAllUsers().ToList()
            };

            return View(vm);
        }
		/// <summary>
		/// IAction method for returning an about view
		/// </summary>
		/// <param name="id">Id for a concentration</param>
		/// <returns></returns>
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
		/// <summary>
		/// IAction method for returning a contact view
		/// </summary>
		/// <param name="id">Id for a concentration</param>
		/// <returns></returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
		/// <summary>
		/// IAction method for returning a privacy view
		/// </summary>
		/// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }
		/// <summary>
		/// IAction method for returning an error view upon a traceable error
		/// </summary>
		/// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
		/// <summary>
		/// IAction method for returning an admin works example view
		/// </summary>
		/// <returns></returns>
        #region Making sure the Roles work
        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return Content("Admin works");
        }
		/// <summary>
		/// IAction method for returning a director works example view
		/// </summary>
		/// <returns></returns>
        [Authorize(Roles = "Director")]
        public IActionResult Director()
        {
            return Content("Director works");
        }
		/// <summary>
		/// IAction method for returning an instructor works example view
		/// </summary>
		/// <returns></returns>
        [Authorize(Roles = "Instructor")]
        public IActionResult Instructor()
        {
            return Content("Instructor works");
        }
		/// <summary>
		/// IAction method for returing an basic user works example view
		/// </summary>
		/// <returns></returns>
        [Authorize(Roles = "Basic")]
        public IActionResult Basic()
        {
            return Content("It works");
        }
        #endregion

    }
}
