using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PagebaTask.DTOs;

namespace PagebaTask.Services.Courses
{
    public interface ICourseService
    {
        IEnumerable<CourseDto> GetAllCourses();
        CourseDto GetCourseById(int id);
        CourseDto AddCourse(CourseDto courseDto);
        CourseDto DeleteCourse(int id);
        CourseDto AddStudentToCourse(int courseId, int studentId);
    }
}