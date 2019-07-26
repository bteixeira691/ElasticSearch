using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCore.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }
    public class Enrollment 
    {
        public Guid EnrollmentID { get; set; }
        public Guid CourseID { get; set; }
        public Guid StudentID { get; set; }
        public Grade? Grade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
