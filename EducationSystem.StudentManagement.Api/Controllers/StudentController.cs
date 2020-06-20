using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Common.ApiUtils;
using EducationSystem.StudentManagement.Application.Commands;
using EducationSystem.StudentManagement.Application.Queries;
using EducationSystem.StudentManagement.Core;
using EducationSystem.StudentManagement.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EducationSystem.StudentManagement.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get student by Id
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Student</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Envelope<StudentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope<StudentDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetStudentByIdQuery(id));
            return FromResult(result);
        }

        /// <summary>
        /// Add a new student
        /// </summary>
        /// <param name="student">Student DTO</param>
        /// <returns>Student</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(NewStudentDto studentDto)
        {
            var result = await _mediator.Send(new CreateStudentCommand(studentDto));
            return FromResult(result);
        }

        /// <summary>
        /// Expose student by Id
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Result</returns>
        [HttpPost("{id}/expose")]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Expose(int id)
        {
            var result = await _mediator.Send(new ExposeStudentCommand(id));
            return FromResult(result);
        }


        /// <summary>
        /// Graduate student by Id
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Result</returns>
        [HttpPost("{id}/graduate")]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Graduate(int id)
        {
            var result = await _mediator.Send(new GraduateStudentCommand(id));
            return FromResult(result);
        }

    }
}
