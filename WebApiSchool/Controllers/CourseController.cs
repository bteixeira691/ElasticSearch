using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using ApplicationSchool.Model;
using ApplicationSchool.Interfaces;

namespace WebApiSchool.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly ICourseService _courseservice;
        private readonly IMapper _mapper;


        public CourseController(ICourseService service, IMapper mapper)
        {

            _mapper = mapper;
            _courseservice = service;
        }

        // GET: api/Course
        [HttpGet]
        public IEnumerable<CourseView> GetAll()
        {
            return _courseservice.GetAllCourses();

        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _courseservice.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);

        }


        // GET: api/Course/5
        [HttpGet("search/{name}")]
        public async Task<IActionResult> AutoCompleter(string name)
        {
            var result = await _courseservice.autoCompleterCourse(name);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);

        }


        // POST: api/Course
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CourseView course)
        {
            await _courseservice.CreateCourse(course);
            return Created("create", "created");

        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCourseAsync(Guid id, [FromBody] CourseView course)
        {
            var result = await _courseservice.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            var res = await _courseservice.EditCourse(id, course);
            return Ok(result);

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var result = await _courseservice.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            await _courseservice.DeleteCourse(id);

            return Ok();
        }


    }


}
