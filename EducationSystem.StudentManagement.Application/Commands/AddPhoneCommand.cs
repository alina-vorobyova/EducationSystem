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
    public class AddPhoneCommand : IRequest<Result>
    {
        public NewPhoneDto NewPhoneDto { get; set; }

        public AddPhoneCommand(NewPhoneDto newPhoneDto)
        {
            NewPhoneDto = newPhoneDto;
        }

        class AddPhoneCommandHandler : IRequestHandler<AddPhoneCommand, Result>
        {
            private readonly IStudentRepository _studentRepository;

            public AddPhoneCommandHandler(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<Result> Handle(AddPhoneCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var student = await _studentRepository.GetByIdAsync(request.NewPhoneDto.StudentId);

                    if(student is null)
                       return  Result.Failure("Can not add phone number, because Student not found");

                    var phoneResult = Phone.Create(request.NewPhoneDto.Number, request.NewPhoneDto.Type);

                    if(phoneResult.IsFailure)
                        return Result.Failure(phoneResult.ErrorMessage);

                    student.AddPhone(phoneResult.Value);

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
