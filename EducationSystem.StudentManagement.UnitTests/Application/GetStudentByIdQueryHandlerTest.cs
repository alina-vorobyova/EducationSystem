using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EducationSystem.Common.ValueObjects;
using EducationSystem.StudentManagement.Application.Profiles;
using EducationSystem.StudentManagement.Application.Queries;
using EducationSystem.StudentManagement.Core;
using FluentAssertions;
using Moq;
using Xunit;

namespace EducationSystem.StudentManagement.UnitTests.Application
{
    public class GetStudentByIdQueryHandlerTest
    {
        private readonly IMapper _mapper;

        public GetStudentByIdQueryHandlerTest()
        {
            var config = new MapperConfiguration(cnf =>
            {
                cnf.AddProfile(new AutoMapperProfile());
            });
            _mapper = config.CreateMapper();
        }

        private Student GetTestStudent()
        {
            return new Student(
                new FullName("Test", "Test"),
                new Passport("123123123"),
                PhotoUrl.Empty,
                Email.Create("mail@mail.com").Value);
        }

        [Fact]
        public async Task Can_get_student_by_Id()
        {
            var query = new GetStudentByIdQuery(1);
            var mock = new Mock<IStudentRepository>();
            mock.Setup(x => x.GetByIdAsync(query.Id)).ReturnsAsync(GetTestStudent());
            var handler = new GetStudentByIdQuery.GetStudentByIdQueryHandler(mock.Object, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Email.Should().Be("mail@mail.com");
        }
    }
}
