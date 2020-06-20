using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EducationSystem.StudentManagement.Application.Queries;
using Xunit;

namespace EducationSystem.StudentManagement.UnitTests.Application
{
    public class GetStudentByIdQueryHandlerTest
    {
        [Fact]
        public async Task Can_get_student_by_Id()
        {
            var getStudentByIdQuery = new GetStudentByIdQuery(1);
            var handler = new GetStudentByIdQuery.GetStudentByIdQueryHandler(null, null);

            var result = await handler.Handle(getStudentByIdQuery, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
        }
    }
}
