


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
    public class CourseService : ICourseService
    {
        private readonly ICourceRepositorySql _icourseSQL;
        private readonly ICourseRepositoryElastic _icourseElastic;
        private readonly IMapper _mapper;
      



        public CourseService(ICourceRepositorySql course, IMapper mapper, ICourseRepositoryElastic elastic)
        {
            _icourseSQL = course;
            _mapper = mapper;
            _icourseElastic = elastic;
        }

        public async Task<CourseView> GetById(Guid id )
        {
            var result = await _icourseSQL.Get(id);

            if (result == null)
                return null;

            return _mapper.Map<CourseView>(result);


        }
        public IEnumerable<CourseView> GetAllCourses()
        {

            return _icourseSQL.GetAll().Select(x => _mapper.Map<CourseView>(x)).ToList() ;

        
        }

        public async Task  CreateCourse(CourseView course)
        {
            Course coursesql = _mapper.Map<Course>(course);

            await _icourseSQL.Add(coursesql);
         
            await _icourseElastic.Add(coursesql);

         

        }

        public async Task<bool> DeleteCourse(Guid id)
        {
            await _icourseSQL.Remove(id);
            await _icourseElastic.Remove(id);
            return true;

        }
           
       
        
        public async Task<CourseView> EditCourse(Guid id, CourseView course)
        {

            course.CourseID = id;
            Course courseDomain = _mapper.Map<Course>(course);
            await _icourseSQL.Update(id , _mapper.Map<Course>(courseDomain));
            await _icourseElastic.Update(id , courseDomain);

            return course;

        }
        
        public async Task ReIndexCourses()
        {

            foreach(var value in _icourseSQL.GetAll())
            {
                await _icourseElastic.Add(value);
            }
        }

        public async Task<List<CourseView>> autoCompleterCourse(string name)
        {
            List<CourseView> courseViews = new List<CourseView>();
            var response = await _icourseElastic.autoCompleterCourse(name);
            foreach (var item in response)
            {
                courseViews.Add(_mapper.Map<CourseView>(item));
            }
            return courseViews;
        }
    }
}
