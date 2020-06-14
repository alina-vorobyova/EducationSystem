using System;
using System.Threading.Tasks;
using EducationSystem.StudentManagement.Core;

namespace EducationSystem.StudentManagement.Infrastructure
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentsDbContext _context;

        public StudentRepository(StudentsDbContext context)
        {
            _context = context;
        }

        public async Task<Student> GetById(int id)
        {
            return await _context.Student.FindAsync(id);
        }

        public async Task Create(Student student)
        { 
            _context.Student.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Student student)
        {
            _context.Student.Update(student);
            await _context.SaveChangesAsync();
        }
    }
}
