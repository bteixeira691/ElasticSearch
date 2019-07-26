using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationSchool.Model
{
    public class CourseView 

    {
        public Guid CourseID { get; set; }

        [Required(ErrorMessage = "Please enter the Title")]
        public string Title { get; set; }

        [Range(1, 12)]
        public int Credits { get; set; }

        public ICollection<EnrollmentView> Enrollments { get; set; }

       
    }
}
