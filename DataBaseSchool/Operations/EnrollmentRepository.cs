using DataBaseSchool.Model;
using StudentCore.Interfaces;
using StudentCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseSchool.Operations
{
    public class EnrollmentRepository : StudentCore<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(SchoolContext context) : base(context)
        {

        }
    }
}
