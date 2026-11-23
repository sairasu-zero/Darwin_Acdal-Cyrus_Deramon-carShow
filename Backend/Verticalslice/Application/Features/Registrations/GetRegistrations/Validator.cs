using FluentValidation;
namespace CarShowRegistration.Application.Features.Registrations.GetRegistrations;
public sealed class GetRegistrationsValidator : AbstractValidator<Query>
{
    public GetRegistrationsValidator()
    {
        RuleFor(x => x.RegistrationType).Must(x => x is null || Enum.IsDefined(x.Value));
        RuleFor(x => x.RegistrationStatus).Must(x => x is null || Enum.IsDefined(x.Value));
    }
}
