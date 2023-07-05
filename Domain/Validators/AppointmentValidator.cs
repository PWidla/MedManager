using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class AppointmentValidator : AbstractValidator<Appointment>
    {
        public AppointmentValidator()
        {
            RuleFor(a => a.Date)
                .NotEmpty().WithMessage("Appointment date is required.");

            RuleFor(a => a.Description)
                .NotEmpty().WithMessage("Appointment description is required.")
                .MaximumLength(1000).WithMessage("Appointment description cannot be longer than 500 characters.");

            RuleFor(a => a.PatientId)
                .NotEmpty().WithMessage("Patient ID is required.")
                .GreaterThan(0).WithMessage("Invalid patient ID.");

            RuleFor(a => a.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required.")
                .GreaterThan(0).WithMessage("Invalid doctor ID.");
        }
    }
}
