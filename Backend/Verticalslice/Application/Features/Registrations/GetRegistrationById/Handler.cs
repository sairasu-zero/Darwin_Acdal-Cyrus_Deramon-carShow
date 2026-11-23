using CarShowRegistration.Application.Common;
using CarShowRegistration.Application.Features.Registrations;
namespace CarShowRegistration.Application.Features.Registrations.GetRegistrationById;
public sealed class GetRegistrationByIdHandler(IRegistrationRepository repository)
{
    public async Task<Result<Response>> HandleAsync(Query query, CancellationToken cancellationToken = default)
    {
        var x = await repository.GetByIdAsync(query.Id, cancellationToken);
        if (x is null)
            return Result<Response>.Failure(ResultError.NotFound(
                "Registration.NotFound",
                "Registration was not found."));

        return Result<Response>.Success(new Response(
            x.Id,
            x.RegistrationNumber,
            x.RegistrationDate,
            x.ParticipantName,
            x.ContactNumber,
            x.Email,
            x.Address,
            x.CarMakeModel,
            x.PlateNumber,
            x.RegistrationType,
            x.Status,
            x.RegistrationFee,
            x.AmountPaid,
            x.PaymentStatus));
    }
}
