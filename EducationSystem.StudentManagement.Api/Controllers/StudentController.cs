using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
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
        [ProducesResponseType(typeof(ApiResult<StudentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult<StudentDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResult<StudentDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetStudentByIdQuery(id));
            if (result.Value is null)
                return NotFound("Student not found");
            return FromResult(result);
        }

        /// <summary>
        /// Add a new student
        /// </summary>
        /// <param name="studentDto">Student DTO</param>
        /// <returns>Student</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(NewStudentDto studentDto)
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
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Graduate(int id)
        {
            var result = await _mediator.Send(new GraduateStudentCommand(id));
            return FromResult(result);
        }


        /// <summary>
        /// Remove student by Id
        /// </summary>
        /// <param name="id">Student Id</param>
        /// <returns>Result</returns>
        [HttpDelete("{id}/remove")]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _mediator.Send(new RemoveStudentCommand(id));
            return FromResult(result);
        }

        /// <summary>
        /// Update student info
        /// </summary>
        /// <param name="updateStudentDto">StudentDto</param>
        /// <returns>Result</returns>
        [HttpPut("{id}/update")]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateStudentDto updateStudentDto)
        {
            var result = await _mediator.Send(new UpdateStudentCommand(updateStudentDto));
            return FromResult(result);
        }

        /// <summary>
        /// Add phone to Student
        /// </summary>
        /// <param name="newPhoneDto">NewPhoneDto</param>
        /// <returns>Result</returns>
        [HttpPost("{id}/addPhone")]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPhone(NewPhoneDto newPhoneDto)
        {
            var result = await _mediator.Send(new AddPhoneCommand(newPhoneDto));
            return FromResult(result);
        }

        /// <summary>
        /// Remove phone from student
        /// </summary>
        /// <param name="removePhoneDto">RemovePhoneDto</param>
        /// <returns>Result</returns>
        [HttpDelete("{id}/removePhone")]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemovePhone(RemovePhoneDto removePhoneDto)
        {
            var result = await _mediator.Send(new RemovePhoneCommand(removePhoneDto));
            return FromResult(result);
        }


        /// <summary>
        /// Edit phone for student
        /// </summary>
        /// <param name="editPhoneDto">EditPhoneDto</param>
        /// <returns>Result</returns>
        [HttpPut("{id}/editPhone")]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditPhone(EditPhoneDto editPhoneDto)
        {
            var result = await _mediator.Send(new EditPhoneCommand(editPhoneDto));
            return FromResult(result);
        }


        /// <summary>
        /// Search student 
        /// </summary>
        /// <param name="searchStudentDto">SearchStudentDto</param>
        /// <returns>Result</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Search([FromQuery] SearchStudentDto searchStudentDto)
        {
            var result = await _mediator.Send(new SearchStudentQuery(searchStudentDto));
            if (result.Value is null)
                return NotFound("Students not found");

            return FromResult(result);
        }
    }
}
