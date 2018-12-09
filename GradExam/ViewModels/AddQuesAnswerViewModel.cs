/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: AddQuesAnswerViewModel.cs 
// Description: Functions as the main view model for QuesAnswer. Handles fields for adding a QuesAnswer object in terms of
//				of a view-model relationship.
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GradExam.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GradExam.ViewModels
{
    public class AddQuesAnswerViewModel
    {
        [Key]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }

        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<LearnOutcome> LearnOutcomes { get; set; }


        public int CourseId { get; set; }
        public int LearnOutcomeId { get; set; }

        public Course Course { get; set; }
        public LearnOutcome LearnOutcome { get; set; }

    }
}
