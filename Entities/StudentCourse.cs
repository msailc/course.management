using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagebaTask.Entities
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}