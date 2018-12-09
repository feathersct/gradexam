using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradExam.Models;
using GradExam.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradExam.Controllers
{	/// <summary>
		/// Controller for handling various user functions
		/// </summary>
		/// <param name="id">Id for a concentration</param>
		/// <returns></returns>
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private IGradeExamRepository context;
		/// <summary>
		/// Constructor for creating a connection to IGradeExamRepository
		/// </summary>
		/// <param name="context">connection to IGradeExamRepository</param>
		/// <returns></returns>
        public UserController(IGradeExamRepository context)
        {
            this.context = context;
        }
		/// <summary>
		/// IAction method for returning an index view
		/// </summary>
		/// <returns></returns>
        public IActionResult Index()
        {
            var model = context.ReadAllUsers();
            return View(model);
        }
		/// <summary>
		/// IAction method for returning a Create view
		/// </summary>
		/// <returns></returns>
        public IActionResult Create()
        {
            return View();
		}
		/// <summary>
		/// IAction method for posting details of the creation of a user
		/// </summary>
		/// <param name="User">User to be created</param>
		/// <returns></returns>
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                context.CreateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
		/// <summary>
		/// IAction method for returning an edit view
		/// </summary>
		/// <param name="id">Id for a user</param>
		/// <returns></returns>
        public IActionResult Edit(string id)
        {
            var model = context.Read(id);

            return View(model);
        }
		/// <summary>
		/// IAction method for posting details about an update to a user 
		/// </summary>
		/// <param name="id">Id for a concentration</param>
		/// <param name="user">User that is being updated</param>
		/// <returns></returns>
        [HttpPost]
        public IActionResult Edit(int id, User user)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Edit", id);
            }

            context.Update(id, user);
            return RedirectToAction("Index");
        }

        
    }
}