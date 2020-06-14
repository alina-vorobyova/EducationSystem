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
    public class ExposeStudentCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public ExposeStudentCommand(int id)
        {
            Id = id;
        }

        class ExposeStudentCommandHandler : IRequestHandler<ExposeStudentCommand, Result>
        {
            private readonly IStudentRepository _studentRepository;

            public ExposeStudentCommandHandler(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<Result> Handle(ExposeStudentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var student = await _studentRepository.GetById(request.Id);
                    student.Expose();
                    await _studentRepository.Update(student);
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
