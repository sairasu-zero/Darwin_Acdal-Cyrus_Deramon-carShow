using CarShowRegistration.Domain.Entities;
using FluentValidation;

namespace CarShowRegistration.Application.Features.Registrations.CreateRegistration;

public sealed class CreateRegistrationValidator : AbstractValidator<Request>
{
    public CreateRegistrationValidator()
    {
        RuleFor(x => x.ParticipantName).NotEmpty().MaximumLength(150);
        RuleFor(x => x.ContactNumber)
            .NotEmpty()
            .Matches(@"^09[0-9]{9}$")
            .WithMessage("ContactNumber must match the format 09XXXXXXXXX.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.Address).NotEmpty().MaximumLength(300);
        RuleFor(x => x.CarMakeModel).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PlateNumber).NotEmpty().MaximumLength(15);
        RuleFor(x => x.RegistrationType).IsInEnum();
        RuleFor(x => x.AmountPaid)
            .GreaterThanOrEqualTo(0)
            .Must((request, amountPaid) =>
                Enum.IsDefined(request.RegistrationType) && amountPaid <= Registration.FeeFor(request.RegistrationType))
            .WithMessage("AmountPaid cannot exceed the registration fee for the selected registration type.");
    }
}
