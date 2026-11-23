using CarShowRegistration.Domain.Entities;

namespace CarShowRegistration.Application.Features.Registrations.CreateRegistration;

public sealed record Command(
    string ParticipantName,
    string ContactNumber,
    string Email,
    string Address,
    string CarMakeModel,
    string PlateNumber,
    RegistrationType RegistrationType,
    decimal AmountPaid);
