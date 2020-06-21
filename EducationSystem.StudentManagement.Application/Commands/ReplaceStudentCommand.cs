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
    public class ReplaceStudentCommand : IRequest<Result>
    {
        public StudentDto StudentDto { get; set; }

        public ReplaceStudentCommand(StudentDto studentDto)
        {
            StudentDto = studentDto;
        }

        class ReplaceStudentCommandHandler : IRequestHandler<ReplaceStudentCommand, Result>
        {
            private readonly IStudentRepository _studentRepository;

            public ReplaceStudentCommandHandler(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<Result> Handle(ReplaceStudentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var id = request.StudentDto.Id;

                    var fullName = new FullName(
                        request.StudentDto.FirstName,
                        request.StudentDto.LastName,
                        request.StudentDto.MiddleName ?? string.Empty);

                    var passport = new Passport(request.StudentDto.Passport);

                    var photoUrl = PhotoUrl.Empty;
                    if (!string.IsNullOrWhiteSpace(request.StudentDto.PhotoUrl))
                        photoUrl = new PhotoUrl(request.StudentDto.PhotoUrl);

                    var email = Email.Empty;
                    if (!string.IsNullOrWhiteSpace(request.StudentDto.Email))
                        email = new Email(request.StudentDto.Email);

                    var student = new Student(fullName, passport, photoUrl, email);

                    student.Id = id;

                    await _studentRepository.UpdateAsync(student);
                    return Result.Success();
                }
                catch (Exception ex)
                {
                    return Result.Failure(ex.Message);
                }
            }
        }
    }
}
