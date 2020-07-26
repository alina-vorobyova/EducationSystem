using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Common.ApiUtils;
using EducationSystem.StudentManagement.Dtos;
using FluentValidation;

namespace EducationSystem.StudentManagement.Api.Validators
{
    public class UpdateStudentDtoValidator : AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100)
                .Must(Validation.IsValidName).When(x => x.FirstName != null)
                .WithMessage("Firstname is invalid");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100)
                .Must(Validation.IsValidName).When(x => x.LastName != null)
                .WithMessage("Lastname is invalid");


            RuleFor(x => x.MiddleName)
                .NotEmpty()
                .MaximumLength(100)
                .Must(Validation.IsValidName).When(x => x.MiddleName != null)
                .WithMessage("Middle is invalid");

            RuleFor(x => x.Passport)
                .NotEmpty()
                .MaximumLength(100)
                .Must(Validation.IsValidPassport).When(x => x.Passport != null)
                .WithMessage("Passport is invalid");


            RuleFor(x => x.PhotoUrl)
                .NotEmpty()
                .MaximumLength(200)
                .Must(Validation.IsValidUrl).When(x => x.PhotoUrl != null)
                .WithMessage("Photo url is invalid");

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(200)
                .EmailAddress().When(x => x.Email != null)
                .WithMessage("Email is invalid");

        }
    }
}
