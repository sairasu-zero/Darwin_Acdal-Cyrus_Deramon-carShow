namespace CarShowRegistration.Application.Features.Registrations;

public sealed record RegistrationSummaryData(
    int TotalRegistrations,
    int Confirmed,
    int CheckedIn,
    int Pending,
    int Cancelled,
    decimal TotalFees,
    decimal TotalPayments);
