using GradExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradExam.ViewModels
{
    public class EditQuestionAnswerViewModel
    {
        public QuesAnswer QuesAnswer { get; set; }
        public int CourseId { get; set; }
        public int LearnOutcomeId { get; set; }

        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<LearnOutcome> LearnOutcomes { get; set; }
    }
}
