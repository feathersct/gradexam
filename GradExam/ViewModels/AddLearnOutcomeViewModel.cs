/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: AddLearnOutcomeViewModel.cs 
// Description: Functions as the main view model for LearningOutcome. Handles fields for adding a LearningOutcome object in terms of
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

namespace GradExam.ViewModels
{
    public class AddLearnOutcomeViewModel
    {
        [Key]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Required]
        public string LearningOutcome { get; set; }
        [Required]
        public int QuestionId { get; set; }

    }
}
