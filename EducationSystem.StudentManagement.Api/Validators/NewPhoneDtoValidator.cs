using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Common.ApiUtils;
using EducationSystem.StudentManagement.Dtos;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace EducationSystem.StudentManagement.Api.Validators
{
    public class NewPhoneDtoValidator : AbstractValidator<NewPhoneDto>
    {
        public NewPhoneDtoValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty()
                .MaximumLength(100)
                .Must(Validation.IsValidPhone)
                .WithMessage("Phone is invalid");

            RuleFor(x => x.Type)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
