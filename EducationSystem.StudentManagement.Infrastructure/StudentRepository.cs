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

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Student.FindAsync(id);
        }

        public async Task CreateAsync(Student student)
        { 
            _context.Student.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Student.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
           var studentToDelete = await _context.Student.FindAsync(id);
           if(studentToDelete is null)
               throw new Exception("Student not found!");

           _context.Student.Remove(studentToDelete);
           await _context.SaveChangesAsync();
        }
    }
}
