using CarShowRegistration.Application.Features.Registrations;
using CarShowRegistration.Domain.Entities;
namespace CarShowRegistration.Application.Features.Registrations.GetRegistrationSummary;
public sealed class GetRegistrationSummaryHandler(IRegistrationRepository repository)
{
    public async Task<Response> HandleAsync(Query query, CancellationToken cancellationToken = default)
    {
        var items = await repository.GetAllAsync(cancellationToken);
        var fees = items.Sum(x => x.RegistrationFee);
        var paid = items.Sum(x => x.AmountPaid);
        return new Response(items.Count,
            Enum.GetValues<RegistrationType>().ToDictionary(x => x, x => items.Count(r => r.RegistrationType == x)),
            Enum.GetValues<RegistrationStatus>().ToDictionary(x => x, x => items.Count(r => r.Status == x)),
            Enum.GetValues<PaymentStatus>().ToDictionary(x => x, x => items.Count(r => r.PaymentStatus == x)), fees, paid, fees - paid);
    }
}
