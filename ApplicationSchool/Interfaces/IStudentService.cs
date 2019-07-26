
using ApplicationSchool.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationSchool.Interfaces
{
    public interface IStudentService
    {

        Task CreateStudent(StudentView student);
        Task<bool> DeleteStudent(Guid id);
        IEnumerable<StudentView> GetAllStudents();
        Task<StudentView> GetById(Guid id);
        Task<StudentView> EditStudent(Guid id, StudentView course);
        Task ReIndexStudents();
        Task<List<StudentView>> autoCompleterStudent(string name);

    }
}
