using CarShowRegistration.Domain.Entities;
namespace CarShowRegistration.Application.Features.Registrations.GetRegistrationSummary;
public sealed record Response(int TotalRegistrations, IReadOnlyDictionary<RegistrationType, int> RegistrationsByType, IReadOnlyDictionary<RegistrationStatus, int> RegistrationsByStatus, IReadOnlyDictionary<PaymentStatus, int> RegistrationsByPaymentStatus, decimal TotalExpectedFees, decimal TotalAmountPaid, decimal OutstandingBalance);
