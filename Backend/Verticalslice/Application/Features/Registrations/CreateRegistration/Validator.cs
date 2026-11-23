using FluentValidation;

namespace CarShowRegistration.Application.Features.Registrations.CreateRegistration;

public sealed class CreateRegistrationValidator : AbstractValidator<Request>
{
    public CreateRegistrationValidator()
    {
        RuleFor(x => x.ParticipantName).NotEmpty().MaximumLength(150);
        RuleFor(x => x.ContactNumber)
            .NotEmpty()
            .MaximumLength(20)
            .Matches(@"^(?:\+63|63|0)9\d{9}$")
            .WithMessage("ContactNumber must be a valid Philippine mobile number.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.Address).NotEmpty().MaximumLength(300);
        RuleFor(x => x.CarMakeModel).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PlateNumber).NotEmpty().MaximumLength(15);
        RuleFor(x => x.RegistrationType).IsInEnum();
        RuleFor(x => x.AmountPaid).GreaterThanOrEqualTo(0);
    }
}
