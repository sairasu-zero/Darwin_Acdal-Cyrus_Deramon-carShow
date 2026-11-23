using CarShowRegistration.Application.Common;
using CarShowRegistration.Application.Features.Registrations;
using CarShowRegistration.Domain.Entities;

namespace CarShowRegistration.Application.Features.Registrations.CreateRegistration;

public sealed class CreateRegistrationHandler(IRegistrationRepository repository)
{
    public async Task<Result<Response>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        var registrationFee = Registration.FeeFor(command.RegistrationType);
        var plateNumber = command.PlateNumber.Trim().ToUpperInvariant();
        if (await repository.PlateNumberExistsAsync(plateNumber, cancellationToken))
            return Result<Response>.Failure(ResultError.Conflict(
                "Registration.Conflict",
                $"Plate number {plateNumber} is already registered."));

        var registration = new Registration
        {
            Id = Guid.NewGuid(),
            RegistrationDate = DateTime.UtcNow,
            ParticipantName = command.ParticipantName.Trim(),
            ContactNumber = command.ContactNumber.Trim(),
            Email = command.Email.Trim(),
            Address = command.Address.Trim(),
            CarMakeModel = command.CarMakeModel.Trim(),
            PlateNumber = plateNumber,
            RegistrationType = command.RegistrationType,
            Status = RegistrationStatus.Pending,
            RegistrationFee = registrationFee,
            AmountPaid = command.AmountPaid
        };

        if (!await repository.AddAsync(registration, cancellationToken))
            return Result<Response>.Failure(ResultError.Conflict(
                "Registration.Conflict",
                $"Plate number {plateNumber} is already registered."));

        return Result<Response>.Success(new Response(
            registration.Id,
            registration.RegistrationNumber,
            registration.RegistrationDate,
            registration.ParticipantName,
            registration.ContactNumber,
            registration.Email,
            registration.Address,
            registration.CarMakeModel,
            registration.PlateNumber,
            registration.RegistrationType,
            registration.Status,
            registration.RegistrationFee,
            registration.AmountPaid,
            registration.PaymentStatus));
    }
}
