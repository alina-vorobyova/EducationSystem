using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationSystem.Common.ValueObjects;
using EducationSystem.StudentManagement.Core;

namespace EducationSystem.StudentManagement.Infrastructure
{
    public class FakeStudentRepository : IStudentRepository
    {
        private static readonly List<Student> _students;
        private static int _counter = 1;

        static FakeStudentRepository()
        {
            _students = new List<Student>
            {
                //new Student(new FullName("Alina", "Skripnikova")) { Id = _counter++ },
                //new Student(new FullName("Gleb", "Skripnikov")) { Id = _counter++ },
                //new Student(new FullName("Firdovsy", "都道府県")) { Id = _counter++ },
            };
        }

        public Task<Student> GetByIdAsync(int id)
        {
            var student = _students.FirstOrDefault(x => x.Id == id);

            if (student is null)
                throw new Exception("Student not found!");

            return Task.FromResult(student);
        }

        public Task CreateAsync(Student student)
        {
            //student.Id = _counter++;
            _students.Add(student);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Student student)
        {
            _students.RemoveAll(x => x.Id == student.Id);
            _students.Add(student);

            return Task.CompletedTask;
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
