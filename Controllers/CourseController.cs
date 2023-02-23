using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagebaTask.DTOs;
using PagebaTask.Services.Courses;

namespace PagebaTask.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet]
        public IActionResult GetAllCourses()
        {
            var courses = _courseService.GetAllCourses();

            return Ok(courses);
        }

        [HttpGet("{id}")]
        public IActionResult GetCourseById(int id)
        {
            var course = _courseService.GetCourseById(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        public IActionResult CreateCourse(CourseDto courseDto)
        {
            var course = _courseService.AddCourse(courseDto);

            return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var course = _courseService.DeleteCourse(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost("{courseId}/students/{studentId}")]
public ActionResult<CourseDto> AddStudentToCourse(int courseId, int studentId)
{
    var courseDto = _courseService.AddStudentToCourse(courseId, studentId);

    if (courseDto == null)
    {
        return NotFound();
    }

    return Ok(courseDto);
}

        
    }
}