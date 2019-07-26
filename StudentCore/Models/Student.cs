using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentCore.Models
{
    public class Student
    {
        public Guid StudentID { get; set; }
        public int? Grade { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        [NotMapped]
        public CompletionField Suggest { get; set; }
      
    }
}
