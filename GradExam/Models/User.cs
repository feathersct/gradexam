using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradExam.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
