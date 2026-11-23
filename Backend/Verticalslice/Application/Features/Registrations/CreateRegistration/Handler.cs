using CarShowRegistration.Application.Features.Registrations;
using CarShowRegistration.Domain.Entities;

namespace CarShowRegistration.Application.Features.Registrations.CreateRegistration;

public sealed class CreateRegistrationHandler(IRegistrationRepository repository)
{
    public async Task<Response> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        var registrationFee = command.RegistrationType switch
        {
            RegistrationType.Exhibitor => 3000m,
            RegistrationType.Competitor => 5000m,
            RegistrationType.VIP => 1500m,
            _ => throw new ArgumentOutOfRangeException(nameof(command.RegistrationType))
        };

        if (command.AmountPaid > registrationFee)
            throw new ArgumentOutOfRangeException(nameof(command.AmountPaid), "AmountPaid cannot exceed the server-generated registration fee.");

        var registration = new Registration
        {
            Id = Guid.NewGuid(),
            RegistrationNumber = ($"CS2027-{Guid.NewGuid():N}".ToUpperInvariant())[0..30],
            RegistrationDate = DateTime.UtcNow,
            ParticipantName = command.ParticipantName.Trim(),
            ContactNumber = command.ContactNumber.Trim(),
            Email = command.Email.Trim(),
            Address = command.Address.Trim(),
            CarMakeModel = command.CarMakeModel.Trim(),
            PlateNumber = command.PlateNumber.Trim().ToUpperInvariant(),
            RegistrationType = command.RegistrationType,
            Status = RegistrationStatus.Pending,
            RegistrationFee = registrationFee,
            AmountPaid = command.AmountPaid
        };
        await repository.AddAsync(registration, cancellationToken);
        return new Response(registration.Id, registration.RegistrationNumber, registration.ParticipantName, registration.RegistrationType, registration.Status, registration.PaymentStatus);
    }
}
