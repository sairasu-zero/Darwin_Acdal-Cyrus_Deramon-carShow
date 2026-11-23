using CarShowRegistration.Domain.Entities;
using FluentValidation;

namespace CarShowRegistration.Application.Features.Registrations.GetRegistrations;

public sealed class GetRegistrationsValidator : AbstractValidator<Query>
{
    public GetRegistrationsValidator()
    {
        RuleFor(x => x.ParticipantName).MaximumLength(150);
        RuleFor(x => x.Type).Must(IsValidRegistrationType)
            .WithMessage("Type must be Exhibitor, Competitor, or VIP.");
        RuleFor(x => x.Status).Must(IsValidRegistrationStatus)
            .WithMessage("Status must be Pending, Confirmed, CheckedIn, or Cancelled.");
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 50);
        RuleFor(x => x)
            .Must(query => (long)(query.Page - 1) * query.PageSize <= int.MaxValue)
            .WithName(nameof(Query.Page))
            .WithMessage("The requested page is too large.");
    }

    private static bool IsValidRegistrationType(string? value) =>
        value is null || Enum.GetNames<RegistrationType>().Contains(value, StringComparer.OrdinalIgnoreCase);

    private static bool IsValidRegistrationStatus(string? value) =>
        value is null || Enum.GetNames<RegistrationStatus>().Contains(value, StringComparer.OrdinalIgnoreCase);
}
