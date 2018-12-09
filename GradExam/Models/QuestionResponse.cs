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
