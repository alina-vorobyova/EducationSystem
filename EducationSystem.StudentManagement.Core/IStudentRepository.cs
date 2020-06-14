using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.StudentManagement.Core
{
    public interface IStudentRepository
    {
        Task<Student> GetById(int id);

        Task Create(Student student);

        Task Update(Student student);
    }
}
