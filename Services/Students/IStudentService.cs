using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagebaTask.DTOs;

namespace PagebaTask.Services.Students
{
    public interface IStudentService
{
    IEnumerable<StudentDto> GetAllStudents();
    StudentDto GetStudentById(int id);
    StudentDto GetStudentByIdProcedure(int id);
    StudentDto AddStudent(StudentDto student);
    StudentDto UpdateStudent(int id, StudentDto student);
    StudentDto DeleteStudent(int id);
    }
}