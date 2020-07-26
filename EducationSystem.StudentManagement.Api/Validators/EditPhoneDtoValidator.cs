using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Common.ApiUtils;
using EducationSystem.StudentManagement.Dtos;
using FluentValidation;

namespace EducationSystem.StudentManagement.Api.Validators
{
    public class EditPhoneDtoValidator : AbstractValidator<EditPhoneDto>
    {
        public EditPhoneDtoValidator()
        {
            RuleFor(x => x.OldNumber)
                .NotEmpty()
                .MaximumLength(100)
                .Must(Validation.IsValidPhone)
                .WithMessage("Old number is invalid");

            RuleFor(x => x.OldType)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.NewNumber)
                .NotEmpty()
                .MaximumLength(100)
                .Must(Validation.IsValidPhone)
                .WithMessage("New number is invalid"); 

            RuleFor(x => x.NewType)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
