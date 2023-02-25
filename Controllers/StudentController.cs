using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagebaTask.DTOs;
using PagebaTask.Services.Students;

namespace PagebaTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
            public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public IActionResult GetAllStudents()
    {
        var students = _studentService.GetAllStudents();

        return Ok(students);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        var student = _studentService.GetStudentById(id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpGet("proc/{id}")]
    public IActionResult GetStudentByIdProcedure(int id)
    {
        var student = _studentService.GetStudentByIdProcedure(id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpPost]
    public IActionResult CreateStudent(StudentDto studentDto)
    {
        var student = _studentService.AddStudent(studentDto);

        return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, StudentDto studentDto)
    {
        var student = _studentService.UpdateStudent(id, studentDto);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var student = _studentService.DeleteStudent(id);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);

    }
}
}