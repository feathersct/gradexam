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
