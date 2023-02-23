using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagebaTask.DTOs;
using PagebaTask.Entities;

namespace PagebaTask.Services.Statuses
{
    public class StudentStatusService : IStudentStatusService
    {
        
    private readonly StudentContext _context;
    public StudentStatusService(StudentContext context)
    {
        _context = context;
    }
        public StudentStatusDto AddStudentStatus(StudentStatusDto studentStatus)
        {
            var newStudentStatus = new StudentStatus
            {
                Status = studentStatus.Status
            };
            _context.StudentStatuses.Add(newStudentStatus);
            _context.SaveChanges();

            return new StudentStatusDto
            {
                Id = newStudentStatus.Id,
                Status = newStudentStatus.Status
            };
        }


        public StudentStatusDto DeleteStudentStatus(int id)
        {
            var studentStatus = _context.StudentStatuses.Find(id);
            if (studentStatus == null) return null;
            _context.StudentStatuses.Remove(studentStatus);
            _context.SaveChanges();

            return new StudentStatusDto
            {
                Id = studentStatus.Id,
                Status = studentStatus.Status
            };
        }

        public IEnumerable<StudentStatusDto> GetAllStudentStatuses()
        {
            return _context.StudentStatuses.Select(s => new StudentStatusDto
            {
                Id = s.Id,
                Status = s.Status
            }).ToList();
        }

        public StudentStatusDto GetStudentStatusById(int id)
        {
            var studentStatus = _context.StudentStatuses.Find(id);
            if (studentStatus == null) return null;

            return new StudentStatusDto
            {
                Id = studentStatus.Id,
                Status = studentStatus.Status
            };
        }
    }
}