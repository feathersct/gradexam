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
    /// Controller for creating/editing/deleting a Course
    /// </summary>
    [Authorize(Roles ="Admin,Director")]
    public class ConcentrationController : Controller
    {
        private IGradeExamRepository _context;
		/// <summary>
		/// Parameterized constructor for creating a context to IGradeExamRepository
		/// </summary>
		/// <param name="context">The view model for resetting a password</param>
		/// <returns></returns>
        public ConcentrationController(IGradeExamRepository context)
        {
            _context = context;
        }
		/// <summary>
		/// IAction method for returning an index view
		/// </summary>
        public IActionResult Index()
        {
            var concentrations = _context.ReadAllConcentrations();
            return View(concentrations);
        }
		/// <summary>
		/// IAction method for handling returning an edit view
		/// </summary>
		/// <param name="id">Id for a concentration</param>
		/// <param name="read"></param>
		/// <returns>Handles read only instructions</returns>
        public IActionResult Edit(int id, bool read = false)
        {
            var concentration = _context.ReadConcentration(id);

            var vm = new EditConcentrationViewModel
            {
                ReadOnly = read,
                Id = concentration.Id,
                Title = concentration.Name
            };

            return View(vm);
        }
		/// <summary>
		/// IAction method for posting the results of an Edit, handles validation
		/// </summary>
		/// <param name="id">Id for a concentration</param>
		/// <param name="vm">view model to edit</param>
		/// <returns></returns>
        [HttpPost]
        public IActionResult Edit(int id, EditConcentrationViewModel vm)
        {
            //Make sure the model state is valid
            if (!ModelState.IsValid)
            {
                return View("Edit", vm);
            }

            Concentration concentration;

            // If we are adding a concentration
            if (id == -1)
            {
                concentration = new Concentration
                {
                    Name = vm.Title
                };

                _context.UpdateConcentration(-1, concentration);
            }
            else //Editing a concentration
            {
                concentration = _context.ReadConcentration(id);

                concentration.Name = vm.Title;

                _context.UpdateConcentration(id, concentration);
            }

            return RedirectToAction("Index");
        }
		/// <summary>
		/// IAction method for returning an addition view
		/// </summary>
		/// <returns></returns>
        public IActionResult Add()
        {
            //Create view model with -1 as id to signify we are adding a concentration
            var vm = new EditConcentrationViewModel();
            vm.Id = -1;

            return View("Edit",vm);
        }
		/// <summary>
		/// IAction method for returning a delete view
		/// </summary>
		/// <param name="id">Id for a concentration</param>
		/// <returns></returns>
        public IActionResult Delete(int id)
        {
            var con = _context.ReadConcentration(id);
            if (con == null)
            {
                return RedirectToAction("Index");
            }
            return View(con);
        }
		/// <summary>
		/// IAction method for handling a deletion confirmed.
		/// </summary>
		/// <param name="id">Id for a concentration</param>
		/// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _context.DeleteConcentration(id);
            return RedirectToAction("Index");
        }
    }
}