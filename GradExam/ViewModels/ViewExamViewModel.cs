/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: ViewExamViewModel.cs 
// Description: Functions as the main view model for an Exam. Handles fields for viewing an Exam object in terms of
//				of a view-model relationship.
// 
// Course: CSCI 3250-001 - Software Engineering I  
// Author: Levi Shelton, SHELTONL@etsu.edu 
// Created: December, October 05, 2018 
// Copyright: Scrum Bags, 2018
// 
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using GradExam.Models;
using System.Collections.Generic;

namespace GradExam.ViewModels
{
    public class ViewExamViewModel
    {
        public Exam Exam { get; set; }
        public Dictionary<int, List<QuestionResponse>> Responses { get; set; }
    }
}