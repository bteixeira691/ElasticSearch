
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentCore.Interfaces;
using ApplicationSchool.Model;
using StudentCore.Models;
using ApplicationSchool.Interfaces;

namespace ApplicationSchool.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepositorySql _istudentSql;
        private readonly IStudentRepositoryElastic _istudentElastic;
        private readonly IMapper _mapper;




        public StudentService(IStudentRepositorySql student, IMapper mapper, IStudentRepositoryElastic elastic)
        {
            _istudentSql = student;
            _mapper = mapper;
            _istudentElastic = elastic;
        }

        public async Task<StudentView> GetById(Guid id)
        {
            var result = await _istudentSql.Get(id);

            if (result == null)
                return null;

            return _mapper.Map<StudentView>(result);


        }
        public IEnumerable<StudentView> GetAllStudents()
        {
            return _istudentSql.GetAll().Select(x => _mapper.Map<StudentView>(x)).ToList();

        }

        public async Task CreateStudent(StudentView course)
        {
            Student studentDomain = _mapper.Map<Student>(course);
            await _istudentSql.Add(studentDomain);
            await _istudentElastic.Add(studentDomain);

        }

        public async Task<bool> DeleteStudent(Guid id)
        {
            await _istudentSql.Remove(id);
            await _istudentElastic.Remove(id);
            return true;

        }


        public async Task<StudentView> EditStudent(Guid id, StudentView student)
        {

            student.StudentID = id;
            Student studentDomain = _mapper.Map<Student>(student);
            await _istudentSql.Update(id, _mapper.Map<Student>(studentDomain));
            await _istudentElastic.Update(id, studentDomain);

            return student;

        }

        public async Task ReIndexStudents()
        {
            foreach (var value in _istudentSql.GetAll())
            {
                await _istudentElastic.Add(value);
            }
        }

        public async Task<List<StudentView>> autoCompleterStudent(string name)
        {

            List<StudentView> studentViews = new List<StudentView>();

            var response = await _istudentElastic.autoCompleterStudent(name);

            foreach (var item in response)
            {
                studentViews.Add(_mapper.Map<StudentView>(item));
            }
            return studentViews;
        }
    }
}
