﻿using System;
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
        public int StudentId { get; set; }
        public UpdateStudentDto StudentDto { get; set; }

        public UpdateStudentCommand(int studentId, UpdateStudentDto studentDto)
        {
            StudentId = studentId;
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
                    var studentToUpdate = await _studentRepository.GetByIdAsync(request.StudentId);
                    if(studentToUpdate is null)
                        return Result.Failure("Student to update not found");

                    var fullNameResult = FullName.Create(
                        request.StudentDto.FirstName ?? studentToUpdate.FullName.FirstName,
                        request.StudentDto.LastName ?? studentToUpdate.FullName.LastName,
                        request.StudentDto.MiddleName ?? studentToUpdate.FullName.MiddleName);

                    var passportResult = Passport.Create(request.StudentDto.Passport ?? studentToUpdate.Passport.Number);
                    var photoUrlResult = PhotoUrl.Create(request.StudentDto.PhotoUrl ?? studentToUpdate.PhotoUrl.Url);
                    var emailResult = Email.Create(request.StudentDto.Email ?? studentToUpdate.Email.EmailAddress);

                    var result = Result.Combine(photoUrlResult, emailResult, fullNameResult, passportResult);
                    if (result.IsFailure)
                        return result;

                    studentToUpdate.Rename(fullNameResult.Value);
                    studentToUpdate.ChangePassport(passportResult.Value);
                    studentToUpdate.ChangePhotoUrl(photoUrlResult.Value);
                    studentToUpdate.ChangeEmail(emailResult.Value);

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
