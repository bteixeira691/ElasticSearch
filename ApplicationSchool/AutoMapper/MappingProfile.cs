using ApplicationSchool.Model;
using AutoMapper;

using StudentCore.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationSchool.AutomMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Course, CourseView>();

            CreateMap<CourseView, Course>();

            CreateMap<Student, StudentView>();

            CreateMap<StudentView, Student>();

            CreateMap<AutoCompleter, AutoCompleterService>();

            CreateMap<SearchResponse, SearchResponseView>();
        }

    }
}
