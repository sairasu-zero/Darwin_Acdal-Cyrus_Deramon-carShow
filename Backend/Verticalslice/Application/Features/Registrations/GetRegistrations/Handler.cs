using CarShowRegistration.Application.Features.Registrations;
namespace CarShowRegistration.Application.Features.Registrations.GetRegistrations;
public sealed class GetRegistrationsHandler(IRegistrationRepository repository)
{
    public async Task<IReadOnlyList<Response>> HandleAsync(Query query, CancellationToken cancellationToken = default)
    {
        var items = await repository.GetAsync(query.RegistrationType, query.RegistrationStatus, cancellationToken);
        return items.Select(x => new Response(x.Id, x.RegistrationNumber, x.ParticipantName, x.Email, x.PlateNumber, x.RegistrationType, x.Status, x.PaymentStatus, x.RegistrationFee, x.AmountPaid)).ToList();
    }
}
