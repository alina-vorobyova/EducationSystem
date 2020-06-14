using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EducationSystem.Common.Utils;
using EducationSystem.Common.ValueObjects;
using EducationSystem.StudentManagement.Core;
using EducationSystem.StudentManagement.Dtos;
using MediatR;

namespace EducationSystem.StudentManagement.Application.Commands
{
    public class CreateStudentCommand : IRequest<Result>
    {
        public NewStudentDto StudentDto { get; }

        public CreateStudentCommand(NewStudentDto studentDto)
        {
            StudentDto = studentDto;
        }

        class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Result>
        {
            private readonly IStudentRepository _studentRepository;

            public CreateStudentCommandHandler(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<Result> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
            {
                var fullName = new FullName(
                    request.StudentDto.FirstName,
                    request.StudentDto.LastName,
                    request.StudentDto.MiddleName ?? string.Empty);

                var student = new Student(fullName);

                await _studentRepository.Create(student);

                return Result.Success();
            }
        }
    }
}
