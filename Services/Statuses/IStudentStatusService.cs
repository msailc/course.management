using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagebaTask.DTOs;

namespace PagebaTask.Services.Statuses
{
    public interface IStudentStatusService
    {
        IEnumerable<StudentStatusDto> GetAllStudentStatuses();
        StudentStatusDto GetStudentStatusById(int id);
        StudentStatusDto AddStudentStatus(StudentStatusDto studentStatus);
        StudentStatusDto DeleteStudentStatus(int id);

    }
}