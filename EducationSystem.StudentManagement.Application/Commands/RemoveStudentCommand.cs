using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EducationSystem.Common.Utils;
using EducationSystem.StudentManagement.Core;
using MediatR;

namespace EducationSystem.StudentManagement.Application.Commands
{
    public class RemoveStudentCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public RemoveStudentCommand(int id)
        {
            Id = id;
        }

        class RemoveStudentCommandHandler : IRequestHandler<RemoveStudentCommand, Result>
        {
            private readonly IStudentRepository _studentRepository;

            public RemoveStudentCommandHandler(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<Result> Handle(RemoveStudentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _studentRepository.RemoveAsync(request.Id);
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
