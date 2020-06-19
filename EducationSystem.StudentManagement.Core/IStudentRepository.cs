using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.StudentManagement.Core
{
    public interface IStudentRepository
    {
        Task<Student> GetByIdAsync(int id);

        Task CreateAsync(Student student);

        Task UpdateAsync(Student student);

        Task RemoveAsync(int id);
    }
}
