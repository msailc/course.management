using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagebaTask.Entities
{
    public class StudentStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }

    }
}