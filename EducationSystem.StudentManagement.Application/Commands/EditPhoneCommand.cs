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
    public class EditPhoneCommand : IRequest<Result>
    {
        public EditPhoneDto EditPhoneDto { get; set; }

        public EditPhoneCommand(EditPhoneDto editPhoneDto)
        {
            EditPhoneDto = editPhoneDto;
        }

        class EditPhoneCommandHandler : IRequestHandler<EditPhoneCommand, Result>
        {
            private readonly IStudentRepository _studentRepository;

            public EditPhoneCommandHandler(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<Result> Handle(EditPhoneCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var student = await _studentRepository.GetByIdAsync(request.EditPhoneDto.StudentId);
                    if(student is null)
                        return Result.Failure("Can not edit phone, because student not found");

                    var oldPhone = new Phone(request.EditPhoneDto.OldNumber, request.EditPhoneDto.OldType);
                    var newPhone = new Phone(request.EditPhoneDto.NewNumber, request.EditPhoneDto.NewType);

                    student.ChangePhone(oldPhone, newPhone);

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
