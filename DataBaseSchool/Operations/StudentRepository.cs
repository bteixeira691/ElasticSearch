using DataBaseSchool.Model;
using StudentCore.Interfaces;
using StudentCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseSchool.Operations
{
    public class StudentRepository : StudentCore<Student>, IStudentRepositorySql
    {
        public StudentRepository(SchoolContext context) : base(context)
        {

        }
    }
}
