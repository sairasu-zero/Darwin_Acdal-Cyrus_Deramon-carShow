namespace CarShowRegistration.Application.Features.Registrations.GetRegistrationSummary;

public sealed record Response(
    int TotalRegistrations,
    int Confirmed,
    int CheckedIn,
    int Pending,
    int Cancelled,
    decimal TotalFees,
    decimal TotalPayments,
    decimal UncollectedPayments);
