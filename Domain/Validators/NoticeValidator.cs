using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class NoticeValidator : AbstractValidator<Notice>
    {
        public NoticeValidator()
        {
            RuleFor(d => d.Content)
                .NotEmpty().WithMessage("Content is required.");
        }
    }
}
