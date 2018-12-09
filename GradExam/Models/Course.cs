/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: Course.cs 
// Description: Functions as the main model for Course. Handles fields for a specific Course object.
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
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GradExam.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
        public string Section { get; set; }

        [Display(Name="Instructor")]
        public User Instructor { get; set; }
        public Concentration Concentration { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual List<LearnOutcome> LearnOutcomes { get; set; }
        public virtual List<QuesAnswer> QuesAnswers { get; set; }
    }
}
