using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationSchool.Model
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class EnrollmentView
    {
        public Guid EnrollmentID { get; set; }
        public Guid CourseID { get; set; }
        public Guid StudentID { get; set; }
        public Grade? Grade { get; set; }

        public CourseView Course { get; set; }
        public StudentView Student { get; set; }
    }
}
