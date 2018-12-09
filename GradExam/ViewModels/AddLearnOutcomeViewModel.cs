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
