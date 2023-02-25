using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PagebaTask.DTOs;
using PagebaTask.Entities;

namespace PagebaTask.Services.Students
{
    public class StudentService : IStudentService
{
    private readonly StudentContext _context;

    public StudentService(StudentContext context)
    {
        _context = context;
    }

        public StudentDto AddStudent(StudentDto student)
{
    var newStudent = new Student
    {
        Name = student.Name,
        Surname = student.Surname,
        Year = student.Year,
        StatusId = student.StatusId,
        IndexNo = student.IndexNo
    };
    _context.Students.Add(newStudent);
    _context.SaveChanges();

    return new StudentDto
    {
        Id = newStudent.Id,
        Name = newStudent.Name,
        Surname = newStudent.Surname,
        Year = newStudent.Year,
        StatusId = newStudent.StatusId
    };
}

        public StudentDto DeleteStudent(int id)
{
    var student = _context.Students.Find(id);
    if (student == null) return null;
    _context.Students.Remove(student);
    _context.SaveChanges();

    return new StudentDto
    {
        Id = student.Id,
        Name = student.Name,
        Surname = student.Surname,
        Year = student.Year,
        StatusId = student.StatusId
    };
}

public IEnumerable<StudentDto> GetAllStudents()
{
    var students = _context.Students
        .Include(s => s.Status)
        .Include(s => s.StudentCourses)
        .ThenInclude(sc => sc.Course)
        .ToList();

    return students.Select(s => new StudentDto
    {
        Id = s.Id,
        Name = s.Name,
        Surname = s.Surname,
        IndexNo = s.IndexNo,
        Year = s.Year,
        StatusId = s.StatusId,
        Courses = s.StudentCourses.Select(sc => new CourseDto
        {
            Id = sc.Course.Id,
            Name = sc.Course.Name
        }).ToList()
    });
}
public StudentDto GetStudentById(int id)
{
    var student = _context.Students.Include(s => s.Status)
        .Include(s => s.StudentCourses).ThenInclude(sc => sc.Course)
        .SingleOrDefault(s => s.Id == id);
    if (student == null) return null;
    return new StudentDto
    {
        Id = student.Id,
        Name = student.Name,
        Surname = student.Surname,
        IndexNo = student.IndexNo,
        Year = student.Year,
        StatusId = student.StatusId,
        Courses = student.StudentCourses.Select(sc => new CourseDto
        {
            Id = sc.Course.Id,
            Name = sc.Course.Name
        }).ToList()
    };
}

// Stored procedure GetStudentById (  :((((  )

public StudentDto GetStudentByIdProcedure(int id)
{
    var student = _context.Students
    .FromSqlRaw("EXECUTE GetStudentWithCoursesAndStatus @Id", new SqlParameter("@Id", id))
    .AsEnumerable()
    .Select(s => new StudentDto
    {
        Id = s.Id,
        Name = s.Name,
        Surname = s.Surname,
        IndexNo = s.IndexNo,
        Year = s.Year,
        StatusId = s.StatusId,
        Courses = s.StudentCourses != null 
            ? s.StudentCourses
                .Select(sc => new CourseDto
                {
                    Id = sc.CourseId,
                    Name = sc.Course.Name,
                    Students = null
                })
                .ToList()
            : null
    })
    .FirstOrDefault();

    return student;
}






        public StudentDto UpdateStudent(int id, StudentDto student)
{
    var existingStudent = _context.Students.Find(id);
    if (existingStudent == null) return null;
    existingStudent.Name = student.Name;
    existingStudent.Surname = student.Surname;
    existingStudent.Year = student.Year;
    existingStudent.StatusId = student.StatusId;
    _context.SaveChanges();

    return new StudentDto
    {
        Id = existingStudent.Id,
        Name = existingStudent.Name,
        Surname = existingStudent.Surname,
        Year = existingStudent.Year,
        StatusId = existingStudent.StatusId
    };
}
    }
}