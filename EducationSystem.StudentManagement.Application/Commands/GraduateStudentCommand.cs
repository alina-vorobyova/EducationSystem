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
    public class GraduateStudentCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public GraduateStudentCommand(int id)
        {
            Id = id;
        }


        class GraduateStudentCommandHandler : IRequestHandler<GraduateStudentCommand, Result>
        {
            private readonly IStudentRepository _studentRepository;

            public GraduateStudentCommandHandler(IStudentRepository studentRepository)
            {
                _studentRepository = studentRepository;
            }

            public async Task<Result> Handle(GraduateStudentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var student = await _studentRepository.GetByIdAsync(request.Id);
                    if(student is null)
                        return Result.Failure("Can not Graduate Student, because Student not found");

                    student.Graduate();
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
