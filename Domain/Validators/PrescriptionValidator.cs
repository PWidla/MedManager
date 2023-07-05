using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class PrescriptionValidator : AbstractValidator<Prescription>
    {
        public PrescriptionValidator()
        {
            RuleFor(x => x.Medicines)
                .NotEmpty().WithMessage("Medicines cannot be empty.")
                .MaximumLength(1000).WithMessage("Medicines must be less than 1000 characters long.");

            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Patient ID cannot be empty.");

            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Doctor ID cannot be empty.");

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("Created at date cannot be empty.");

            RuleFor(x => x.ExpiresAt)
                .GreaterThanOrEqualTo(x => x.CreatedAt).WithMessage("Expires at date must be greater than or equal to created at date.");
        }
    }
}
