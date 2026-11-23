using CarShowRegistration.Application.Common;
using CarShowRegistration.Application.Features.Registrations;

namespace CarShowRegistration.Application.Features.Registrations.GetRegistrationSummary;

public sealed class GetRegistrationSummaryHandler(IRegistrationRepository repository)
{
    public async Task<Result<Response>> HandleAsync(Query query, CancellationToken cancellationToken = default)
    {
        var summary = await repository.GetSummaryAsync(cancellationToken);
        return Result<Response>.Success(new Response(
            summary.TotalRegistrations,
            summary.Confirmed,
            summary.CheckedIn,
            summary.Pending,
            summary.Cancelled,
            summary.TotalFees,
            summary.TotalPayments,
            summary.TotalFees - summary.TotalPayments));
    }
}
