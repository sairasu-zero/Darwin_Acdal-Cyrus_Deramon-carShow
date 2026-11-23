using CarShowRegistration.Domain.Entities;
namespace CarShowRegistration.Application.Features.Registrations.GetRegistrationById;
public sealed record Response(Guid Id, string RegistrationNumber, DateTime RegistrationDate, string ParticipantName, string ContactNumber, string Email, string Address, string CarMakeModel, string PlateNumber, RegistrationType RegistrationType, RegistrationStatus Status, PaymentStatus PaymentStatus, decimal RegistrationFee, decimal AmountPaid);
