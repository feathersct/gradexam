using GradExam.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradExam.ViewModels
{
    public class EditLearnOutcomeViewModel
    {
        public LearnOutcome LearningOutcome { get; set; }
        public int QuestionId { get; set; }
    }
}
