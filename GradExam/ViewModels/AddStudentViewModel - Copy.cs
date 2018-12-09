/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: AddStudentViewModel.cs 
// Description: Functions as the main view model for Student. Handles fields for adding a Student object in terms of
//				of a view-model relationship.
// 
// Course: CSCI 3250-001 - Software Engineering I  
// Author: Levi Shelton, SHELTONL@etsu.edu 
// Created: December, October 05, 2018 
// Copyright: Scrum Bags, 2018
// 
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.ComponentModel.DataAnnotations;
namespace GradExam.ViewModels
{
    public class AddStudentViewModel
    {
        [Required]
        public string Name { get; set; }
        

    }
}