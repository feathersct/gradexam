/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: EditCourseViewModel.cs 
// Description: Functions as the main view model for Course. Handles fields for editing a Course object in terms of
//				of a view-model relationship.
// 
// Course: CSCI 3250-001 - Software Engineering I  
// Author: Levi Shelton, SHELTONL@etsu.edu 
// Created: December, October 05, 2018 
// Copyright: Scrum Bags, 2018
// 
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using GradExam.Models;

namespace GradExam.ViewModels
{
    public class EditCourseViewModel
    {
        public Course Course { get; set; }
        public string UserId { get; set; }
    }
}