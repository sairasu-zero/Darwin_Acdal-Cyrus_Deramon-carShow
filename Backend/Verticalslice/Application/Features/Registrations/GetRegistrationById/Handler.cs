using CarShowRegistration.Application.Features.Registrations;
namespace CarShowRegistration.Application.Features.Registrations.GetRegistrationById;
public sealed class GetRegistrationByIdHandler(IRegistrationRepository repository)
{
    public async Task<Response?> HandleAsync(Query query, CancellationToken cancellationToken = default)
    {
        var x = await repository.GetByIdAsync(query.Id, cancellationToken);
        return x is null ? null : new Response(x.Id, x.RegistrationNumber, x.RegistrationDate, x.ParticipantName, x.ContactNumber, x.Email, x.Address, x.CarMakeModel, x.PlateNumber, x.RegistrationType, x.Status, x.PaymentStatus, x.RegistrationFee, x.AmountPaid);
    }
}
