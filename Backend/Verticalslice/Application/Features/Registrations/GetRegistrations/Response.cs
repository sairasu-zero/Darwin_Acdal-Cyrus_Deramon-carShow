using CarShowRegistration.Domain.Entities;

namespace CarShowRegistration.Application.Features.Registrations.GetRegistrations;

public sealed record Response(
    IReadOnlyList<ItemResponse> Items,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages);

public sealed record ItemResponse(
    Guid Id,
    string RegistrationNumber,
    string ParticipantName,
    string Email,
    string PlateNumber,
    RegistrationType RegistrationType,
    RegistrationStatus Status,
    PaymentStatus PaymentStatus,
    decimal RegistrationFee,
    decimal AmountPaid);
