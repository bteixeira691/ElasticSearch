using ApplicationSchool.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationSchool.Interfaces
{
    public interface ICourseService
    {

        Task CreateCourse(CourseView student);
        Task<bool> DeleteCourse(Guid id);
        IEnumerable<CourseView> GetAllCourses();
        Task<CourseView> GetById(Guid id);
        Task<CourseView> EditCourse(Guid id, CourseView course);
        Task ReIndexCourses();
        Task<List<CourseView>> autoCompleterCourse(string name);
    }
}
