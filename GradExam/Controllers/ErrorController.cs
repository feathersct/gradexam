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