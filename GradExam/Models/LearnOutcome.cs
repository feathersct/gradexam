/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: LearnOutcome.cs 
// Description: Functions as the main model for LearnOutcome. Handles fields for a specific LearnOutcome object
// 
// Course: CSCI 3250-001 - Software Engineering I  
// Author: Levi Shelton, SHELTONL@etsu.edu 
// Created: December, October 05, 2018 
// Copyright: Scrum Bags, 2018
// 
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradExam.Models
{
    public class LearnOutcome
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Learning Outcome")]
        public string LearningOutcome { get; set; }
        
        [Display(Name = "Question")]
        public List<QuesAnswer> QuestionAnswer { get; set; }


    }
}
