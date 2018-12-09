/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
// 
// Solution/Project: GradExam/GradExam
// File Name: EditConcentrationViewModel.cs 
// Description: Functions as the main view model for Concentration. Handles fields for editing a Concentration object in terms of
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradExam.ViewModels
{
    public class EditConcentrationViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
