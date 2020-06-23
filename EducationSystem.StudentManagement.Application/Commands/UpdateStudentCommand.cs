using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
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
    public class UpdateStudentCommand : IRequest<Result>
    {
        public UpdateStudentDto StudentDto { get; set; }

        public UpdateStudentCommand(UpdateStudentDto studentDto)
        {
            StudentDto = studentDto;
        }

        class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Result>
        {
            private readonly IStudentRepository _studentRepository;

            public UpdateStudentCommandHandler(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<Result> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var studentToUpdate = await _studentRepository.GetByIdAsync(request.StudentDto.Id);
                    if(studentToUpdate is null)
                        return Result.Failure("Student to update not found");

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

                    studentToUpdate.ChangeEmail(email);
                    studentToUpdate.ChangePassport(passport);
                    studentToUpdate.ChangePhotoUrl(photoUrl);
                    studentToUpdate.Rename(fullName);

                    await _studentRepository.UpdateAsync(studentToUpdate);

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
