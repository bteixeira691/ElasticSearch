
using StudentCore.Interfaces;
using StudentCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentCore.Interfaces

{
    public interface IStudentRepositoryElastic : IStudentCore<Student>
    {
        Task<List<Student>> autoCompleterStudent(string input);
    }
}
