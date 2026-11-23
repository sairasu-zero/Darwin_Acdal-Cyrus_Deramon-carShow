using CarShowRegistration.Domain.Entities;

namespace CarShowRegistration.Application.Features.Registrations;

public interface IRegistrationRepository
{
    Task<Registration?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Registration> Items, int TotalCount)> GetAsync(
        string? participantName,
        RegistrationType? type,
        RegistrationStatus? status,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
    Task<RegistrationSummaryData> GetSummaryAsync(CancellationToken cancellationToken = default);
    Task<bool> PlateNumberExistsAsync(string plateNumber, CancellationToken cancellationToken = default);
    Task<bool> AddAsync(Registration registration, CancellationToken cancellationToken = default);
}
