using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Common.ApiUtils;
using EducationSystem.StudentManagement.Dtos;
using FluentValidation;

namespace EducationSystem.StudentManagement.Api.Validators
{
    public class RemovePhoneDtoValidator : AbstractValidator<RemovePhoneDto>
    {
        public RemovePhoneDtoValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty()
                .MaximumLength(100)
                .Must(Validation.IsValidPhone)
                .WithMessage("Phone to remove is invalid");
        }
    }
}
