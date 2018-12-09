using System.ComponentModel.DataAnnotations;
namespace GradExam.ViewModels
{
    public class AddStudentViewModel
    {
        [Required]
        public string Name { get; set; }
        

    }
}