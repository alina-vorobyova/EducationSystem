﻿using EducationSystem.Common.ApiUtils;
using EducationSystem.StudentManagement.Dtos;
using FluentValidation;

namespace EducationSystem.StudentManagement.Api.Validators
{
    public class NewStudentDtoValidator : AbstractValidator<NewStudentDto>
    {
        public NewStudentDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100)
                .Must(Validation.IsValidName);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100)
                .Must(Validation.IsValidName);

            RuleFor(x => x.MiddleName)
                .MaximumLength(100)
                .Must(Validation.IsValidName).When(x => x.MiddleName != null);

            RuleFor(x => x.Passport)
                .NotEmpty()
                .MaximumLength(100)
                .Must(Validation.IsValidPassport);

            RuleFor(x => x.PhotoUrl)
                .MaximumLength(200)
                .Must(Validation.IsValidUrl).When(x => x.PhotoUrl != null);

            RuleFor(x => x.Email)
                .MaximumLength(200)
                .EmailAddress().When(x => x.Email != null);
        }
    }
}
