using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;


using Microsoft.AspNetCore.Authorization;
using StudentCore.Interfaces;
using AutoMapper;
using StudentCore.Models;
using ApplicationSchool.Model;
using ApplicationSchool.Interfaces;

namespace WebApiSchool.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
     

        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;

        private readonly IMapper _mapper;


        public IndexController(ICourseService courseService  , IStudentService studentService)
        {

            _courseService = courseService;
            _studentService = studentService;
        }

       
        // POST: api/Course
        [HttpPost]
        public async Task<IActionResult> ReIndexAll()
        {
            await _courseService.ReIndexCourses();
            await _studentService.ReIndexStudents();
            return Ok();

        }

      
    }


}
