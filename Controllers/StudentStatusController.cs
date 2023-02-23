using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagebaTask.DTOs;
using PagebaTask.Services.Statuses;

namespace PagebaTask.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StudentStatusController : ControllerBase
    {
         private readonly IStudentStatusService _studentStatusService;
            public StudentStatusController(IStudentStatusService studentStatusService)
    {
        _studentStatusService = studentStatusService;
    }
        [HttpGet]
        public IActionResult GetAllStudentStatuses()
        {
            var studentStatuses = _studentStatusService.GetAllStudentStatuses();

            return Ok(studentStatuses);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentStatusById(int id)
        {
            var studentStatus = _studentStatusService.GetStudentStatusById(id);

            if (studentStatus == null)
            {
                return NotFound();
            }

            return Ok(studentStatus);
        }

        [HttpPost]
        public IActionResult CreateStudentStatus(StudentStatusDto studentStatusDto)
        {
            var studentStatus = _studentStatusService.AddStudentStatus(studentStatusDto);

            return CreatedAtAction(nameof(GetStudentStatusById), new { id = studentStatus.Id }, studentStatus);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudentStatus(int id)
        {
            var studentStatus = _studentStatusService.DeleteStudentStatus(id);

            if (studentStatus == null)
            {
                return NotFound();
            }

            return Ok(studentStatus);
        }
    }
}