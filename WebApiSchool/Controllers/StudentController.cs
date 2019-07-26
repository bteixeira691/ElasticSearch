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
    public class StudentController : ControllerBase
    {

        private readonly IStudentService _studentService;

        public StudentController(IStudentService service)
        {
            _studentService = service;
        }

        // GET: api/Course
        [HttpGet]
        public IEnumerable<StudentView> GetAll()
        {
            var res = _studentService.GetAllStudents();
            return _studentService.GetAllStudents();
        }

        // GET: api/Course/5
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _studentService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Course
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] StudentView student)
        {
            await _studentService.CreateStudent(student);
            return Created("create", "created");
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCourseAsync(Guid id, [FromBody] StudentView student)
        {
            var result = await _studentService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            var res = await _studentService.EditStudent(id, student);
            return Ok(result);

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var result = await _studentService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            await _studentService.DeleteStudent(id);

            return Ok();
        }

        
        [HttpGet("search/{name}")]
        public async Task<IActionResult> AutoCompleter(string name)
        {
            var result = await _studentService.autoCompleterStudent(name);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);

        }

    }


}
