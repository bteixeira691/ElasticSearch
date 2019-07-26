using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentCore.Models
{
    public class Course
    {
        public Guid CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        [NotMapped]
        public CompletionField Suggest { get; set; }
       
    }
}
