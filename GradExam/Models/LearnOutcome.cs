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
