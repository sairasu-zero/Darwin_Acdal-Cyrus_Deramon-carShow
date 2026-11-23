using CarShowRegistration.Domain.Entities;

namespace CarShowRegistration.Application.Features.Registrations.CreateRegistration;

public sealed record Response(Guid Id, string RegistrationNumber, string ParticipantName, RegistrationType RegistrationType, RegistrationStatus Status, PaymentStatus PaymentStatus);
