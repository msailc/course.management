using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagebaTask.DTOs
{
    public class StudentDto
    {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int IndexNo { get; set; }
    public int Year { get; set; }
    public int StatusId { get; set; }
    public List<CourseDto> Courses { get; set; }
    }
}