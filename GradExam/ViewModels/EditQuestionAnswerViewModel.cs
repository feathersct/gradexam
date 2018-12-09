/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: EditQuestionAnswerViewModel.cs 
// Description: Functions as the main view model for QuesAnswer. Handles fields for editing a QuesAnswer object in terms of
//				of a view-model relationship.
// 
// Course: CSCI 3250-001 - Software Engineering I  
// Author: Levi Shelton, SHELTONL@etsu.edu 
// Created: December, October 05, 2018 
// Copyright: Scrum Bags, 2018
// 
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using GradExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradExam.ViewModels
{
    public class EditQuestionAnswerViewModel
    {
        public QuesAnswer QuesAnswer { get; set; }
        public int CourseId { get; set; }
        public int LearnOutcomeId { get; set; }

        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<LearnOutcome> LearnOutcomes { get; set; }
    }
}
