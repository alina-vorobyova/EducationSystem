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
    public class RemovePhoneCommand : IRequest<Result>
    {
        public int StudentId { get; set; }
        public RemovePhoneDto RemovePhoneDto { get; set; }

        public RemovePhoneCommand(int studentId, RemovePhoneDto removePhoneDto)
        {
            StudentId = studentId;
            RemovePhoneDto = removePhoneDto;
        }

        class RemovePhoneCommandHandler : IRequestHandler<RemovePhoneCommand, Result>
        {
            private readonly IStudentRepository _studentRepository;

            public RemovePhoneCommandHandler(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<Result> Handle(RemovePhoneCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var student = await _studentRepository.GetByIdAsync(request.StudentId);

                    if(student is null)
                        return Result.Failure("Can not remove phone because student not found");

                    student.RemovePhone(request.RemovePhoneDto.Number);

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
