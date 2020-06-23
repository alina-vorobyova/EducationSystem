using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Common.Utils;
using EducationSystem.StudentManagement.Core;
using EducationSystem.StudentManagement.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.StudentManagement.Application.Queries
{
    public class SearchStudentQuery : IRequest<Result<IEnumerable<StudentDto>>>
    {
        public SearchStudentDto SearchStudentDto { get; set; }

        public SearchStudentQuery(SearchStudentDto searchStudentDto)
        {
            SearchStudentDto = searchStudentDto;
        }

        public class SearchStudentQueryHandler : IRequestHandler<SearchStudentQuery, Result<IEnumerable<StudentDto>>>
        {
            private readonly IStudentRepository _studentRepository;
            private readonly IMapper _mapper;

            public SearchStudentQueryHandler(IStudentRepository studentRepository, IMapper mapper)
            {
                _studentRepository = studentRepository;
                _mapper = mapper;
            }

            public async Task<Result<IEnumerable<StudentDto>>> Handle(SearchStudentQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var allStudents = _studentRepository.GetAllStudents();

                    var firstName = request.SearchStudentDto.FirstName;
                    var lastName = request.SearchStudentDto.LastName;
                    var middleName = request.SearchStudentDto.MiddleName;

                    if (!string.IsNullOrWhiteSpace(firstName))
                        allStudents = allStudents.Where(x => x.FullName.FirstName.Contains(firstName));

                    if(!string.IsNullOrWhiteSpace(lastName))
                        allStudents = allStudents.Where(x => x.FullName.LastName.Contains(lastName));

                    if (!string.IsNullOrWhiteSpace(middleName))
                        allStudents = allStudents.Where(x => x.FullName.MiddleName.Contains(middleName));

                    var result = await allStudents.ToListAsync(cancellationToken);

                    var studentDtoList = _mapper.Map<IEnumerable<StudentDto>>(result);
                    return Result.Success(studentDtoList);

                }
                catch (Exception ex)
                {
                    return Result.Failure<IEnumerable<StudentDto>>(ex.Message);
                }
            }
        }
    }
}
