using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PagebaTask.DTOs;
using PagebaTask.Entities;

namespace PagebaTask.Services.Courses
{
    public class CourseService : ICourseService
    {
        private readonly StudentContext _context;

        public CourseService(StudentContext context)
        {
            _context = context;
        }

        public CourseDto AddCourse(CourseDto courseDto)
        {
            var newCourse = new Course
            {
                Name = courseDto.Name
            };

            _context.Courses.Add(newCourse);
            _context.SaveChanges();

            return new CourseDto
            {
                Id = newCourse.Id,
                Name = newCourse.Name

            };
        }

        public CourseDto DeleteCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return null;
            _context.Courses.Remove(course);
            _context.SaveChanges();

            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name
            };
        }

        public IEnumerable<CourseDto> GetAllCourses()
{
    return _context.Courses.Select(c => new CourseDto
    {
        Id = c.Id,
        Name = c.Name,
        Students = c.StudentCourses
            .Select(sc => new StudentDto
            {
                Id = sc.Student.Id,
                Name = sc.Student.Name,
                Surname = sc.Student.Surname,
                IndexNo = sc.Student.IndexNo,
                Year = sc.Student.Year,
                StatusId = sc.Student.StatusId
            }).ToList()
    }).ToList();
}

        public CourseDto GetCourseById(int id)
{
    var course = _context.Courses
        .Include(c => c.StudentCourses)
        .ThenInclude(sc => sc.Student)
        .FirstOrDefault(c => c.Id == id);

    if (course == null)
        return null;

    return new CourseDto
    {
        Id = course.Id,
        Name = course.Name,
        Students = course.StudentCourses
            .Select(sc => new StudentDto
            {
                Id = sc.Student.Id,
                Name = sc.Student.Name,
                Surname = sc.Student.Surname,
                IndexNo = sc.Student.IndexNo,
                Year = sc.Student.Year,
                StatusId = sc.Student.StatusId
            })
            .ToList()
    };
}

public CourseDto AddStudentToCourse(int courseId, int studentId)
{
    var course = _context.Courses
        .Include(c => c.StudentCourses)
        .ThenInclude(sc => sc.Student)
        .FirstOrDefault(c => c.Id == courseId);

    var student = _context.Students.Find(studentId);

    if (course == null || student == null)
    {
        return null;
    }

    if (course.StudentCourses.Any(sc => sc.StudentId == studentId))
    {
        return new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            Students = course.StudentCourses
                .Select(sc => new StudentDto
                {
                    Id = sc.Student.Id,
                    Name = sc.Student.Name,
                    Surname = sc.Student.Surname,
                    IndexNo = sc.Student.IndexNo,
                    Year = sc.Student.Year,
                    StatusId = sc.Student.StatusId
                })
                .ToList()
        };
    }

    var studentCourse = new StudentCourse
    {
        CourseId = courseId,
        StudentId = studentId
    };

    _context.StudentCourses.Add(studentCourse);
    _context.SaveChanges();

    return new CourseDto
    {
        Id = course.Id,
        Name = course.Name,
        Students = course.StudentCourses
            .Select(sc => new StudentDto
            {
                Id = sc.Student.Id,
                Name = sc.Student.Name,
                Surname = sc.Student.Surname,
                IndexNo = sc.Student.IndexNo,
                Year = sc.Student.Year,
                StatusId = sc.Student.StatusId
            })
            .ToList()
    };
}


    }
}