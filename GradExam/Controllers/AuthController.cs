/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: AuthController.cs 
// Description: Functions as the main controller for the auth page of the program. Handles various actions of the page
//              such as displaying an index page, logging in and out a user, register actions, reseting a password, etc
//              via various page redirections and or functions. 
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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GradExam.Controllers
{
	/// <summary>
    /// Controller for Authentication management and various actions involving authentication for a user.
    /// </summary>
    public class AuthController : Controller
    {
        private IGradeExamRepository context;
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private RoleManager<IdentityRole> roleManager;
		/// <summary>
        /// Parameterized constructor for AuthController, sets up context for calling IGradeExamRepository and fields involving a user. 
        /// </summary>
        /// <param name="context">repository for calling methods</param>
        /// <param name="userManager">Manages users</param>
		/// <param name="signInManager">Manages sign ins</param>
        /// <param name="roleManager">Manages roles</param>
        public AuthController(IGradeExamRepository context, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
		/// <summary>
        /// IAction method for returning an index view 
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        #region LogIn Actions
		/// <summary>
        /// IAction method for returning a login view 
        /// </summary>
        public IActionResult LogIn()
        {
            var vm = new LogInViewModel();

            return View(vm);
        }
        /// <summary>
        /// IAction method for posting a login view. Handles validation for inputs. 
        /// </summary>
        [HttpPost]
        public IActionResult LogIn(LogInViewModel vm)
        {
            var viewModel = new LogInViewModel
            {
                Username = vm.Username,
                Password = vm.Password
            };

            // If the user entered in invalid data
            if (!ModelState.IsValid)
                return View("Login", viewModel);

            // Try to log in the user
            var user = LogInUser(viewModel.Username, viewModel.Password).Result;

            // User sign in failed
            if (!user.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "The username and/or password is incorrect. Please Try Again.");

                return View("Login", viewModel);
            }

            // If succeeded go to home index
            return RedirectToAction("Index", "Home", null);
  
        }
        #endregion
		/// <summary>
        /// Handles Logging out a user
        /// </summary>
        /// <param name="vm">The viewmodel for resetting a password</param>
        /// <returns></returns>
        #region LogOut Actions
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home", null);
        }
        #endregion

        #region Register Actions
		 /// <summary>
        /// IAction method for getting and returning a Register view. 
        /// </summary>
        [HttpGet]
        public IActionResult Register()
        {
            var vm = new RegisterViewModel();

            return View(vm);
        }
		/// <summary>
        /// Register a user for usage, handles user creation validation.
        /// </summary>
        /// <param name="vm">The viewmodel for registering a user</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {

            // Validate the model
            if (!ModelState.IsValid)
            {
                // Get the view model and return it
                return View("Register", vm);
            }

            // Compare to see if the two passwords are the same
            if(vm.Password != vm.ConfirmPassword)
            {
                // Get the view model and return it with validation error: "Passwords do not match"
                return Content("Passwords do not match");
            }

            var userName = vm.Email.Split('@')[0];
            var userByEmailExists = userManager.Users.Any(user => user.Email == vm.Email);
            var userByUserNameExists = userManager.Users.Any(user => user.UserName == userName);

            // See if there is a user with the same username/email in the system
            if (userByUserNameExists || userByUserNameExists)
            {
                // Get the view model and return it with validation error: "Email already used for an existing user"
                ModelState.AddModelError(string.Empty, "User already exists in the system. Please use a different email address.");
                return View("Register", vm);
            }

            // Try to make user
            var newUser = new User
            {
                UserName = userName,
                Email = vm.Email,
                FirstName = vm.FirstName,
                LastName = vm.LastName
            };

            var result = await userManager.CreateAsync(newUser, vm.Password);


            // Handle any other exceptions
            if (result.Succeeded)
            {
                if(!await roleManager.RoleExistsAsync("Basic"))
                {
                    var role = new IdentityRole("Basic");
                    var res = await roleManager.CreateAsync(role);

                    if(res.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newUser, "Basic");
                        await LogInUser(userName, vm.Password);
                    }

                }

                return RedirectToAction("Index", "Home", null);
            }

            // For some reason the system could not create the user. Redirect back to create.
            ModelState.AddModelError(string.Empty, "Sorry, the system could not create the user requested. Try again later.");
            return View("Register", vm);
        }
        #endregion

        #region Reset Password
        [HttpGet]
        public IActionResult ResetPassword()
        {
            var vm = new ResetPasswordViewModel();

            return View();
        }
		/// <summary>
        /// Reset a password for a selected user, password validation
        /// </summary>
        /// <param name="vm">The viewmodel for resetting a password</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            var user = userManager.GetUserAsync(User).Result;
            var passwordCorrect = await userManager.CheckPasswordAsync(user, vm.OldPassword);
            var newPasswordSame = vm.ConfirmNewPassword == vm.NewPassword;

            if (!passwordCorrect)
            {
                ModelState.AddModelError("", "Password not correct for this user");
                return View("ResetPassword", vm);
            }

            if(!newPasswordSame)
            {
                ModelState.AddModelError("", "New passwords not the same");
                return View("ResetPassword", vm);
            }
                

            var change = await userManager.ChangePasswordAsync(user, vm.OldPassword, vm.NewPassword);
            
            if(change.Succeeded)
            {
                return RedirectToAction("Index", "Home", null);
            }

            ModelState.AddModelError("", "Failed for some reason, come back and try later");
            return View(vm);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Get the curent user
        /// </summary>
        /// <returns></returns>
        private Task<User> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
        
        /// <summary>
        /// Log in the user
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <param name="password">The password of the user</param>
        /// <returns></returns>
        private async Task<Microsoft.AspNetCore.Identity.SignInResult> LogInUser(string username, string password)
        {
            // Try to log in the user
            return await signInManager.PasswordSignInAsync(username, password, true, false);
        }
        #endregion

    }
}