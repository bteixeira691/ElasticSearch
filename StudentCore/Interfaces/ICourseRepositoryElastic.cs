
using StudentCore.Interfaces;
using StudentCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentCore.Interfaces

{
    public interface ICourseRepositoryElastic : IStudentCore<Course>
    {
        Task<List<Course>> autoCompleterCourse(string input);
    }
}
