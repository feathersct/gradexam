/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: Concentration.cs 
// Description: Functions as the main model for Concentration. Handles fields for a specific Concentration object
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
    public class Concentration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
