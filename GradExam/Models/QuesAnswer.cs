using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradExam.Models
{
    public class QuesAnswer 
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }

        [Display(Name = "Course")]
        public Course Course { get; set; }
   
        [Display(Name = "Learning Outcome")]
        public LearnOutcome LearnOutcome { get; set; }
    }
}
