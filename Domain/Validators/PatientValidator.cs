using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class PatientValidator : AbstractValidator<Patient>
    {
        public PatientValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(u => u.EmailAddress)
                 .NotEmpty().WithMessage("Email address is required.")
                 .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

            RuleFor(u => u.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .Must(dob => dob < DateTime.UtcNow.AddYears(-18)).WithMessage("You must be at least 18 years old.");
        }
    }
}
