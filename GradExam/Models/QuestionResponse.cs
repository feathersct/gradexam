/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: Concentration.cs 
// Description: Functions as the main model for QuestionResponse. Handles fields for a specific QuestionResponse object.
// 
// Course: CSCI 3250-001 - Software Engineering I  
// Author: Levi Shelton, SHELTONL@etsu.edu 
// Created: December, October 05, 2018 
// Copyright: Scrum Bags, 2018
// 
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradExam.Models
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public QuesAnswer Question { get; set; }
        public string Response { get; set; }
        [Range(1,10)]
        public int Rating { get; set; }
        public Exam Exam { get; set; }
        public Course Course { get; set; }
    }
}
