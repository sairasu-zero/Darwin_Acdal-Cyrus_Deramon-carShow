using CarShowRegistration.Domain.Entities;
namespace CarShowRegistration.Application.Features.Registrations.GetRegistrations;
public sealed record Response(Guid Id, string RegistrationNumber, string ParticipantName, string Email, string PlateNumber, RegistrationType RegistrationType, RegistrationStatus Status, PaymentStatus PaymentStatus, decimal RegistrationFee, decimal AmountPaid);
