using System.ComponentModel.DataAnnotations;
namespace GradExam.ViewModels
{
    public class AddCourseViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Semester { get; set; }
        [Required]
        public string Section { get; set; }
        [Required]
        public string InstructorName { get; set; }

        public string Concentration { get; set; }

    }
}