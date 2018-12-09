/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: AddCourseViewModel.cs 
// Description: Functions as the main view model for Course. Handles fields for adding a Course object in terms of
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
    public class AddCourseViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Semester { get; set; }
        [Required]
        public string Section { get; set; }
        [Required]
        public string InstructorName { get; set; }

        public string Concentration { get; set; }

    }
}