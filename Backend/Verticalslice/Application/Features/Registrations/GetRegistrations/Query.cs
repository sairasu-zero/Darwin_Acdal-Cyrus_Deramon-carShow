namespace CarShowRegistration.Application.Features.Registrations.GetRegistrations;

public sealed record Query(
    string? ParticipantName = null,
    string? Type = null,
    string? Status = null,
    int Page = 1,
    int PageSize = 10);
