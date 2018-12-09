using GradExam.Models;
using System.Collections.Generic;

namespace GradExam.ViewModels
{
    public class ViewExamViewModel
    {
        public Exam Exam { get; set; }
        public Dictionary<int, List<QuestionResponse>> Responses { get; set; }
    }
}