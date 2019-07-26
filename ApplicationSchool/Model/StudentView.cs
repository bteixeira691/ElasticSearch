using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationSchool.Model
{
    public class StudentView
    {
        public Guid StudentID { get; set; }

        [Required(ErrorMessage = "Please enter the LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter the FirstName")]
        public string FirstName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public ICollection<EnrollmentView> Enrollments { get; set; }
    }
}
