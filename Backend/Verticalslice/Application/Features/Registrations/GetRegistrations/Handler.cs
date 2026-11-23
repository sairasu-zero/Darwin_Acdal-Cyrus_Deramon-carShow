using CarShowRegistration.Application.Common;
using CarShowRegistration.Application.Features.Registrations;
using CarShowRegistration.Domain.Entities;

namespace CarShowRegistration.Application.Features.Registrations.GetRegistrations;

public sealed class GetRegistrationsHandler(IRegistrationRepository repository)
{
    public async Task<Result<Response>> HandleAsync(Query query, CancellationToken cancellationToken = default)
    {
        var registrationType = query.Type is not null
            ? Enum.Parse<RegistrationType>(query.Type, ignoreCase: true)
            : (RegistrationType?)null;
        var registrationStatus = query.Status is not null
            ? Enum.Parse<RegistrationStatus>(query.Status, ignoreCase: true)
            : (RegistrationStatus?)null;

        var (registrations, totalCount) = await repository.GetAsync(
            query.ParticipantName?.Trim(),
            registrationType,
            registrationStatus,
            query.Page,
            query.PageSize,
            cancellationToken);

        var items = registrations.Select(x => new ItemResponse(
            x.Id,
            x.RegistrationNumber,
            x.ParticipantName,
            x.Email,
            x.PlateNumber,
            x.RegistrationType,
            x.Status,
            x.PaymentStatus,
            x.RegistrationFee,
            x.AmountPaid)).ToList();

        var totalPages = (int)Math.Ceiling((double)totalCount / query.PageSize);
        return Result<Response>.Success(new Response(items, query.Page, query.PageSize, totalCount, totalPages));
    }
}
