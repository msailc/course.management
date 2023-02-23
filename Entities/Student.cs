using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PagebaTask.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int IndexNo { get; set; }
        public int Year { get; set; }
        public int StatusId { get; set; }
        public StudentStatus Status { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }

    }
}